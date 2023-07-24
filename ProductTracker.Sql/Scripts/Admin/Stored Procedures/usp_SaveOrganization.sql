
Create Procedure [dbo].[usp_SaveOrganization]
(
	@OrgName varchar(200), 
	@AliasName Varchar(4),
	@DBPath Varchar(200),
	@DeActivationDate Datetime,
	@CreatedBy int
)
AS
Begin
	
	Insert into [dbo].[Organization](OrgName,AliasName, DBPath,IsActive, DeActivationDate,CreatedBy, CreatedOn)
	Values (@OrgName,@AliasName,@DBPath,1,@DeActivationDate, @CreatedBy, GetDate())

End
