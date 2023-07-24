
Create Procedure [dbo].[usp_SaveUser]  
(  
 @Id int,
 @FName Varchar(50),  
 @MName Varchar(50),  
 @LName Varchar(50),  
 @Email Varchar(50),
 @IsActive bit,  
 @Password Varchar(50),  
 @CreatedBy Varchar(50)  
)  
AS  
Begin  
 If(@Id > 0)
 Begin
 Update [User] Set 
 [FName]= @FName,
 [MName] = @MName,
 [LName] = @LName,
 [Email] = @Email,
 [Password]= @Password,
 [IsActive] = @IsActive
 
 Where Id = @id
 End
 Else
 Begin
 Insert into [User] ([FName],[MName],[LName],[Email],[Password],[IsActive],[CreatedBY],[CreatedOn])   
  Values (@FName,@MName,@LName,@Email,@Password,1,@CreatedBy, Getdate())  
End
End