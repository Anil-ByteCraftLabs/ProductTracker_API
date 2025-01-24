Create Procedure [dbo].[usp_GetTemplates]  
As 
Begin  
Select TemplateId Id, TemplateName [Name], OrgId, IsDefault, IsActive, TempFormat,
CreatedOn, CreatedBy, UpdatedBy, UpdatedOn from Template where IsActive =1  

End 