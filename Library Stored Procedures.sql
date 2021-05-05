-- =============================================
-- Author:		Chaise Allegra
-- Description:	Return all books
-- =============================================
CREATE PROCEDURE GetAllBooks  
AS  
BEGIN  
    SET NOCOUNT ON;  
    Select * from Books  
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Search books
-- =============================================
CREATE PROCEDURE SearchBooks(@Name VarChar(Max),@Desc VarChar(Max), @Count Int,@Genre VarChar(50),@Author VarChar(50))
AS  
BEGIN  
   SELECT FROM WHERE @Name=na
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Add New Book
-- =============================================
CREATE PROCEDURE AddNewBook(@Name VarChar(Max),@Desc VarChar(Max), @Count Int,@Genre VarChar(50),@Author VarChar(50),@Total Int)
AS  
BEGIN  
   INSERT INTO Books (Name,Description,Count,Author,Genre,Total) VALUES (@Name,@Desc,@Count,@Author,@Genre,@Total)  
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Update Book
-- =============================================
CREATE PROCEDURE CheckOutBook(@amount Int,@bookName VarChar(Max))
AS  
BEGIN  
   UPDATE Books SET Count = Count - @amount WHERE Count-@amount >-1 AND Name = @bookName 
END   
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Check In Book
-- =============================================
CREATE PROCEDURE CheckInBook(@amount Int,@bookName VarChar(Max))
AS  
BEGIN  
   UPDATE Books SET Count = Count + @amount WHERE Count+@amount <Total+1 AND Name = @bookName 
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Return all user check out books
-- =============================================
CREATE PROCEDURE GetAllUserBooks  (@Name VarChar(Max))
AS  
BEGIN  
    SET NOCOUNT ON;  
    Select * from CheckedOut WHERE @Name=UserName
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Add Info to the check out table
-- =============================================
CREATE PROCEDURE InsertCheckOut(@Name VarChar(Max),@BookName VarChar(Max), @Count Int)
AS  
BEGIN  
   INSERT INTO CheckedOut (UserName,BookName,Count) VALUES (@Name,@BookName,@Count)  
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Delete empty values from table
-- =============================================
CREATE PROCEDURE DeleteFromCheckOutTable
AS  
BEGIN  
   DELETE FROM CheckedOut WHERE Count=0
END  
GO   
 -- =============================================
-- Author:		Chaise Allegra
-- Description:	Update via subtraction 
-- =============================================
CREATE PROCEDURE UpdateCheckOutSub(@Name VarChar(Max),@BookName VarChar(Max), @Count Int)
AS  
BEGIN  
   UPDATE CheckedOut SET Count =Count - @Count WHERE Count-@Count >-1 AND @Name=UserName AND @BookName=BookName
END  
GO   
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Update via addition
-- =============================================
CREATE PROCEDURE UpdateCheckOutAdd(@Name VarChar(Max),@BookName VarChar(Max), @Count Int)
AS  
BEGIN  
   UPDATE CheckedOut SET Count =Count + @Count WHERE @Name=UserName AND @BookName=BookName
END  
GO   
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Insert User
-- =============================================
CREATE PROCEDURE InsertUser(@UserName VarChar,@Password VarChar, @LoggedIn Int)
AS  
BEGIN  
   INSERT INTO Users(UserName,Password,LoggedIn) VALUES (@UserName,@Password,@LoggedIn)  
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Update User
-- =============================================
CREATE PROCEDURE UpdateUser(@user VarChar(Max),@status Int)
AS  
BEGIN  
   UPDATE Users SET LoggedIn = @status WHERE UserName = @user and Password= @password
END  
GO  
-- =============================================
-- Author:		Chaise Allegra
-- Description:	Find User
-- =============================================
CREATE PROCEDURE FindUser(@user VarChar(Max),@password VarChar(Max))
AS  
BEGIN  
   SELECT * FROM Users WHERE UserName = @user and Password= @password
END  
GO  