
CREATE PROC [dbo].[Report] 
 @p_startdate AS DATE = '2008-01-01', @p_finishdate AS DATE = '2008-12-31'

AS
BEGIN
 SET NOCOUNT ON;
 SET XACT_ABORT ON;  
 
 DECLARE @temp dbo.MemoryReportTable;
 DECLARE @p_id AS INT, @p_name AS VARCHAR(25), @p_lvl AS INT, @p_code AS INT 

 DECLARE @t_id AS INT, @t_name AS VARCHAR(25), @t_lvl AS INT ,
 @t_startdate AS DATE, @t_finishdate AS DATE, @t_state AS INT,
 @t_description  AS INT

 DECLARE @outer_cursor CURSOR,  @inner_cursor CURSOR
 DECLARE @sum AS INT, @count AS INT, @p_state AS VARCHAR(25)
 DECLARE @fetch_outer_cursor AS INT , @fetch_inner_cursor AS INT
 DECLARE outer_cursor cursor static local for
 
 SELECT 
      p_id,
      p_code,
      p_name,
      p_lvl,
      p_startdate,
      p_finishdate
  FROM [dbo].[Projects]
 
 BEGIN TRY     
  OPEN outer_cursor
  FETCH NEXT FROM outer_cursor into @p_id, @p_code, @p_name, @p_lvl, @p_startdate, @p_finishdate
  
  SELECT @fetch_outer_cursor = @@FETCH_STATUS
  WHILE @fetch_outer_cursor = 0
  BEGIN
       SELECT @sum = SUM(C.t_state), @count= COUNT(C.p_id) FROM dbo.Tasks as C WHERE C.p_id = @p_id   
       IF (@count = @sum)       
       SET @p_state = 'Completed';              
       IF(@count > @sum )
       SET @p_state = 'inProgres'; 
       IF(@sum = 0 )
       SET @p_state = 'Planned';
           
       INSERT INTO @temp (p_id, p_code, p_name, p_lvl, p_startdate, p_finishdate,p_state) 
              VALUES(@p_id, @p_code, @t_name, @p_lvl, @p_startdate, @p_finishdate, @p_state)                        
     
  SET @inner_cursor  = CURSOR STATIC LOCAL FOR
    SELECT t_id,
        t_name,        
        t_description,
        t_lvl,
        t_startdate,
        t_finishdate,
        t_state
    FROM dbo.Tasks WHERE p_id = @p_id

 OPEN @inner_cursor
 FETCH NEXT FROM @inner_cursor into @t_id, @t_name,@t_description, @t_lvl, @t_startdate, @t_finishdate, @t_state
 SET @fetch_inner_cursor = @@FETCH_STATUS
 WHILE @fetch_inner_cursor = 0
     BEGIN      
      INSERT INTO @temp (t_id, t_name, t_description, t_lvl, t_startdate, t_finishdate,t_state) 
             VALUES(@t_id, @t_name,@t_description, @t_lvl, @t_startdate, @t_finishdate, @t_state)    
                
     FETCH NEXT FROM @inner_cursor into @t_id, @t_name, @t_description, @t_lvl, @t_startdate, @t_finishdate,@t_state
     SET @fetch_inner_cursor = @@FETCH_STATUS   

     END
     CLOSE @inner_cursor
     DEALLOCATE @inner_cursor

     FETCH NEXT FROM outer_cursor into  @p_id, @p_code, @p_name, @p_lvl, @p_startdate, @p_finishdate
     SET @fetch_outer_cursor = @@FETCH_STATUS

  END
  CLOSE outer_cursor
  DEALLOCATE outer_cursor 
     END TRY
     BEGIN CATCH
      IF @@TRANCOUNT > 0       
       RAISERROR ('Attempt to insert value in  "MemoryReportTable" memory Table',16,1)
   END CATCH   
--  DROP TYPE MemoryReportTable
  SELECT * FROM @temp
  SET NOCOUNT OFF;
  SET XACT_ABORT OFF;
END