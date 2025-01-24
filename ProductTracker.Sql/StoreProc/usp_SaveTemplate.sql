Create Procedure [dbo].[usp_SaveTemplate] 
(  
	@OrgId int,  
	@IsDefault bit,  
	@TempName nvarchar(max),  
	@IsActive Bit = 0,
	@CreatedBy nvarchar(200)
) 
AS 
BEGIN

INSERT into Template (OrgId, IsDefault, Templatename, IsActive, CreatedOn, CreatebBy) 
Values  (@OrgId,@IsDefault,@TempName, @IsActive, GETDATE(), @CreatedBy)   

END 
