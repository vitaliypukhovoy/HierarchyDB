CREATE TABLE dbo.Projects
(
  p_id   INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
  p_code INT NOT NULL,
  p_hid     HIERARCHYID NOT NULL,
  p_lvl AS p_hid.GetLevel() PERSISTED,
  p_name VARCHAR(25) NOT NULL,
  p_startdate  DATE  NOT NULL,
  p_finishdate DATE NOT NULL   
)
GO