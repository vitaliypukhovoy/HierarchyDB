
CREATE TABLE dbo.Tasks(
  t_id  INT NOT NULL IDENTITY(1,1) ,
  p_id  INT NOT NULL ,
  t_name VARCHAR(25)  NULL,
  t_description VARCHAR(50) NULL,
  t_hid HIERARCHYID NOT NULL,
  t_lvl AS t_hid.GetLevel() PERSISTED,
  t_startdate  DATE  NOT NULL,
  t_finishdate DATE NOT NULL,
  t_state INT NOT NULL,  
  PRIMARY KEY (t_id), CONSTRAINT FK_ProjetTask FOREIGN KEY (p_id)
  REFERENCES Projects(p_id)
  ON UPDATE NO ACTION ON DELETE CASCADE
)