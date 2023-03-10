.                                   ***DOCUMENTATION***
                       *(soon will be implemented deploy on kubernetes and Azure)*
   
   :white_check_mark: this is have done
   
   1. For realize this small project was chosen .net Core 3.1 Framework and for connection to Database I have used Dapper Framework   
   This is give me to do query directly. Main logic of report was developed on server side (MSSQL server)  
   I desided on to the maximum using sql scripts for better performence. In project was used CRUD an approach.  
  
   2. Project was divided on 5 subprojects. In order to deploy local this project need to open PMS.HierarchyDB.  
   To  check of deploying scripts we can open SQL Server and run Hierarchy.DB.publish.xml (PMS.HierarchyDB project)  
   to create PMSHierarchyDB in local Database.  
  
   To  deploy of Docker container need to run docker-compose.yaml file in root of project: command **docker-compose up --build**.  
   After first build of "docker-compose" will be created two Docker container so next time will need to run command:**docker-compose up**  
   or **docker-compose up d** (without logs).
  
  ***After this actions will be formed two "image" and two "runnung" containers which builded as "image" and running as "container":  
      a. pms-web - where is located  webapi application( inclided 3 projects)  
      b. mssql-db - mssql Server for linux container***  
   
   3. To run this Project need to run PMS.WebAPI subproject.
   Primarily I am using Fiddler for debugging CRUD and other query to App.   
   Here was realized all CRUD operations.  

   For example  __POST__ body of query to Project controllers  
   ```command: http://localhost:8080/api/Project```  
   ```POST```
   ```
   {     
    "p_code" : 21212,    
    "p_mgrid" : 0,                                     //id: 1,2,3 for subproject    
    "p_name" : "Sub Project",    
    "p_startdate" : "2008-01-01",    
    "p_finishdate" :  "2008-01-01"      
   } 
   ```
     
   "p_mgrid" - this is interesting field in query.  
    When "p_mgrid" = 0 we can create individual Projects independet from each other.  
    And when we indicate here number of other project then will be created new subproject which  erlier indicated. nn
    This an aproach  also implemented in POST Tasks query for creating new subtasks.  

   ***a. After ctreating  of new Project  will be also created  new task realated  with  parent project.*** 
   ***b. And when we want to create new task we specify in "t_mgrid" id task which has already been created and have new subtask.***  
    
   For example  __POST__ body of query to Task controllers   
   ```command: http://localhost:8080/api/Task```
   ```POST```
   ```
   {    
    "p_id" : 1,    
    "t_mgrid" : 1,                                     // need to specify this id for parent Task      
    "t_name" : "Sub Project",      
    "t_startdate" : "2008-01-01",      
     "t_finishdate" :  "2008-01-01"      
    }    
   ```
     
   4. The main part of this challenge implemented in Controler ReportController and sql script Report.  
   In Report script was fuced all logic of report.  
   ReportController use Get query we are using it can get Report in xlsx format.  
   ```command: http://localhost:8080/api/Report?startDate=1-12-2000&finishDate=12-12-2012 ```   
   ```GET```

   :package:  planning to do
 
 5. Unwraping Kubernetes, deploy microservices and deploying it to Azure AKS
    
    
  
 

