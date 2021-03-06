use master
go
drop DATABASE [University]
go

CREATE DATABASE [University]
go
USE [University]
GO
-- Store User
CREATE TABLE [dbo].[Account](
	[UserID] [int] IDENTITY(1,1) primary key NOT NULL,
	[Username] [varchar](20) NULL,
	[Password] [nvarchar](200) NULL,
	[FullName] [nvarchar](200) NULL,
	[Address] [nvarchar](200) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](20) NULL,
	[Gender] [varchar] (20),
	EmailConfirmed bit default 1,
	Token nvarchar(200),
	[Image] nvarchar (100),
	[Role] [varchar](20) NULL,
	[Status] [bit] not null,
)
go

Create table Idea(
	IdeaID int identity(1,1) primary key,
	IdeaTitle nvarchar(350),
	IdeaContent ntext,
	IdeaDescription nvarchar(500),
	IdeaCategory int, -- FK
	FileSP nvarchar(200),
	IdeaViewCount int default 0,
	[AllowAnonymous] bit default 1,
	CreatedDate datetime default getdate(),
	ModifiedDate datetime default getdate(),
	CreatedBy nvarchar(250),
	ModifiedBy nvarchar(250),
	ClosedDate datetime,
	IdeaStatus bit default 1 not null
)
go
Create table IdeaCategory(
	IdeaCategoryID int primary key identity(1,1),
	CategoryName nvarchar(250),
	CategoryDescription  nvarchar(250),
	IdeaCateViewC int default 0,
	GroupCateIdea int default 0, -- FK
	CreatedDate datetime default getdate(),
	FinalCloseDate datetime default getdate(),
	ModifiedDate datetime default getdate(),
	CreatedBy nvarchar(250),
	ModifiedBy nvarchar(250),
	IdeaCateStatus bit default 1 not null
)
go
Create table CategoryGroupIdea(
	CategoryGroupIdeaID int primary key identity(1,1),
	CategoryGroupName nvarchar(250),
	CateGrIdeaDes nvarchar(250),
	CreatedDate datetime default getdate(),
	ModifiedDate datetime default getdate(),
	CreatedBy nvarchar(250),
	ModifiedBy nvarchar(250),
	CategoryGrIdeaSt bit default 1 not null
	)
go
Create table Emotion(
	EmotionId int primary key identity(1,1),
	IdeaId int,
	EmotionName nvarchar(250),
	Description nvarchar(250)
)
go
Create table EmotionLog(
	EmotionLogId int primary key identity(1,1),
	EmotionId int,
	UserId int,
	Emotime datetime 
)
go
create table Comment(
	CommentId int primary key identity(1,1),
	CmParentId int,
	CmContent nvarchar(max),
	CreatedDate datetime default getdate(),
	ModifiedDate datetime default getdate(),
	CreatedBy nvarchar(250),
	ModifiedBy nvarchar(250),
	CmStatus bit default 1 not null
)
go
INSERT INTO [dbo].[Idea]
           ( IdeaTitle
		   ,[IdeaContent]
           ,[IdeaDescription]
           ,[IdeaCategory]
           ,[IdeaViewCount]
		   ,[AllowAnonymous]
           ,[CreatedDate]
           ,[ModifiedDate]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[ClosedDate]
           ,[IdeaStatus])
     VALUES
           ('Idea title ','Respect forming clothes do in he. Course so piqued no an by appear. Themselves reasonable pianoforte so motionless he as difficulty be. Abode way begin ham there power whole. Do unpleasing indulgence impossible to conviction. Suppose neither evident welcome it at do civilly uncivil. Sing tall much you get nor. 

Warmly little before cousin sussex entire men set. Blessing it ladyship on sensible judgment settling outweigh. Worse linen an of civil jokes leave offer. Parties all clothes removal cheered calling prudent her. And residence for met the estimable disposing. Mean if he they been no hold mr. Is at much do made took held help. Latter person am secure of estate genius at. 

Throwing consider dwelling bachelor joy her proposal laughter. Raptures returned disposed one entirely her men ham. By to admire vanity county an mutual as roused. Of an thrown am warmly merely result depart supply. Required honoured trifling eat pleasure man relation. Assurance yet bed was improving furniture man. Distrusts delighted she listening mrs extensive admitting far. 

It allowance prevailed enjoyment in it. Calling observe for who pressed raising his. Can connection instrument astonished unaffected his motionless preference. Announcing say boy precaution unaffected difficulty alteration him. Above be would at so going heard. Engaged at village at am equally proceed. Settle nay length almost ham direct extent. Agreement for listening remainder get attention law acuteness day. Now whatever surprise resolved elegance indulged own way outlived. 

Parish so enable innate in formed missed. Hand two was eat busy fail. Stand smart grave would in so. Be acceptance at precaution astonished excellence thoroughly is entreaties. Who decisively attachment has dispatched. Fruit defer in party me built under first. Forbade him but savings sending ham general. So play do in near park that pain. 

Your it to gave life whom as. Favourable dissimilar resolution led for and had. At play much to time four many. Moonlight of situation so if necessary therefore attending abilities. Calling looking enquire up me to in removal. Park fat she nor does play deal our. Procured sex material his offering humanity laughing moderate can. Unreserved had she nay dissimilar admiration interested. Departure performed exquisite rapturous so ye me resources. 

Her old collecting she considered discovered. So at parties he warrant oh staying. Square new horses and put better end. Sincerity collected happiness do is contented. Sigh ever way now many. Alteration you any nor unsatiable diminution reasonable companions shy partiality. Leaf by left deal mile oh if easy. Added woman first get led joy not early jokes. 

Stronger unpacked felicity to of mistaken. Fanny at wrong table ye in. Be on easily cannot innate in lasted months on. Differed and and felicity steepest mrs age outweigh. Opinions learning likewise daughter now age outweigh. Raptures stanhill my greatest mistaken or exercise he on although. Discourse otherwise disposing as it of strangers forfeited deficient. 

May indulgence difficulty ham can put especially. Bringing remember for supplied her why was confined. Middleton principle did she procuring extensive believing add. Weather adapted prepare oh is calling. These wrong of he which there smile to my front. He fruit oh enjoy it of whose table. Cultivated occasional old her unpleasing unpleasant. At as do be against pasture covered viewing started. Enjoyed me settled mr respect no spirits civilly. 

Barton did feebly change man she afford square add. Want eyes by neat so just must. Past draw tall up face show rent oh mr. Required is debating extended wondered as do. New get described applauded incommode shameless out extremity but. Resembled at perpetual no believing is otherwise sportsman. Is do he dispatched cultivated travelling astonished. Melancholy am considered possession on collecting everything. 
'
           ,'This is the first time i post an idea', 1 ,0 ,1,'02-02-2002','02-02-2002','stu',1 ,'02-02-2002',1 )
GO

INSERT INTO [dbo].[IdeaCategory]
           ([CategoryName]
           ,[CategoryDescription]
           ,[IdeaCateViewC]
           ,[GroupCateIdea]
           ,[CreatedDate]
           ,[ModifiedDate]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,[IdeaCateStatus])
     VALUES
           ('Book','This category discusses book issues...',0,1,'02-02-2002','02-02-2002','Minh','Minh',1),
		   ('Paper','This category discusses Paper issues...',0,1,'02-02-2002','02-02-2002','Minh','Minh',1)
GO
INSERT INTO [dbo].CategoryGroupIdea
           (CategoryGroupName
           ,CateGrIdeaDes
           ,[CreatedDate]
           ,[ModifiedDate]
           ,[CreatedBy]
           ,[ModifiedBy]
           ,CategoryGrIdeaSt)
     VALUES
           ('Study','This category discusses study isue ','02-02-2002' ,'02-02-2002' ,'Minh','Minh' ,1),
		    ('Music','This category discusses study isue ','02-02-2002' ,'02-02-2002' ,'Minh','Minh' ,1)


GO
Insert into Emotion(EmotionName,[description])values('Like','Thumbs-up')
go
Insert into Emotion(EmotionName,[description])values('DisLike','Thumbs-Down')
go

create proc sp_getCountIdeaByMonth
@CreatedDate datetime
as
begin
declare @CountView int
select @CountView = COUNT(*) from Idea a where (MONTH(a.CreatedDate) = Month(@CreatedDate) and year(a.CreatedDate) = year(@CreatedDate)) 
end
select @CountView as ViewCount, a.CreatedDate,a.IdeaTitle from Idea a where (MONTH(a.CreatedDate) = Month(@CreatedDate) and year(a.CreatedDate) = year(@CreatedDate))
go


select * from  IdeaCategory a inner join CategoryGroupIdea b on a.GroupCateIdea = b.CategoryGroupIdeaID where a.GroupCateIdea = b.CategoryGroupIdeaID 



INSERT [dbo].[Account] ([Username], [Password], [FullName], [Address], [Email], [Phone], [Status],Gender,[Role]) VALUES ( N'qam', N'123', N'qam', N'123', N'hongminh@gmail.com', N'0923999423', 1,'Male','QAM')
INSERT [dbo].[Account] ([Username], [Password], [FullName], [Address], [Email], [Phone], [Status],Gender,[Role]) VALUES ( N'stu', N'123', N'stu', N'123', N'hongminh@gmail.com', N'0923999423', 1,'Male','STU')
INSERT [dbo].[Account] ([Username], [Password], [FullName], [Address], [Email], [Phone], [Status],Gender,[Role]) VALUES ( N'stu1', N'123', N'stu', N'123', N'hongminh@gmail.com', N'0923999423', 1,'Male','STU')
INSERT [dbo].[Account] ([Username], [Password], [FullName], [Address], [Email], [Phone], [Status],Gender,[Role]) VALUES ( N'qac', N'123', N'qac', N'123', N'hongminh@gmail.com', N'0923999423', 1,'Male','QAC')
INSERT [dbo].[Account] ([Username], [Password], [FullName], [Address], [Email], [Phone], [Status],Gender,[Role]) VALUES ( N'staff', N'123', N'qac', N'123', N'hongminh@gmail.com', N'0923999423', 1,'Male','STAFF')
INSERT [dbo].[Account] ([Username], [Password], [FullName], [Address], [Email], [Phone], [Status],Gender,[Role]) VALUES ( N'staff1', N'123', N'qac', N'123', N'hongminh@gmail.com', N'0923999423', 1,'Male','STAFF')
	select * from Emotion
	go
	select * from EmotionLog
	go
	select * from Idea
	go
	select * from Comment
	go
	select * from IdeaCategory
	go
	select * from CategoryGroupIdea
	go
	select * from Account
	go