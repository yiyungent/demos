create database UserDb
go
use UserDb
go
create table UserInfo
(
	uID int constraint PK_UserInfo_uID primary key identity(1,1) not null,
	uCode nvarchar(32) constraint UQ_UserInfo_uCode unique not null,
	uPassword nvarchar(32) not null,
	uName nvarchar(48) default(N'ÄäÃû') not null,
	uPhone nvarchar(32) null,
	uEmail nvarchar(32) null,
	uDelFlag smallint default(0) not null,
	constraint CK_UserInfo_uEmail check(uEmail like '%@%')
)
go