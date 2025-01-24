Create Procedure [dbo].[usp_SaveTemplateFormat]  
(
	@Id int = 0,
	@UpdatedBy varchar(50),
	@Format nvarchar(max)
)
As 
Begin  
If (@id > 0)
Begin
	Update Template set TempFormat = @Format, UpdatedBy=@UpdatedBy, UpdatedOn= getdate() where TemplateId = @Id
End

End 

