CREATE DATABASE MICROSERVICE;

USE MICROSERVICE;

CREATE TABLE TAB_CHAT (
   ID UNIQUEIDENTIFIER NOT NULL,
   SENDER_NM VARCHAR(20) NOT NULL,
   RECIPIENT_NM VARCHAR(20) NOT NULL,
   MOMENT_DT DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
   MESSAGE_DS VARCHAR(2000) NOT NULL  
);

INSERT INTO TAB_CHAT (ID,SENDER_NM,RECIPIENT_NM,MESSAGE_DS) VALUES
('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C1','Peter','Mary', 'Hello')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C2','Paul','Mary', 'Hello honey')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C3','Mary','Peter','Hi')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C4','Mary','Paul', 'Hello dear')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C5','July','Mary', 'Hey')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C6','Paul','July', 'No Thanks')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C7','July','Paul', 'Ok')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C8','Paul','Peter','Lets?')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659C9','Paul','Peter','Drink one? :)')
,('E35DE0E0-0E9C-4ED9-BDEA-7403DD3659D0','Peter','Paul','Great Idea!');