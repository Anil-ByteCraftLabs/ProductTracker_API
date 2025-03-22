CREATE Procedure usp_GetFilteredUserScannedCoupon --'wewewewe'      
(      
 @Userid varchar(50) ,  
 @OrgId int = 0,  
 @StartDate Date = null,  
 @EndDate Date = null  
)      
As      
Begin      
  Declare @OrgCode Varchar(10)= Null  
  
  Select @OrgCode = AliasName From Admin.Dbo.Organization where Id = @OrgId  
    
 Select Id, CouponCode, ScannedOn, OrgAlias,   Location.Lat AS Latitude, Location.Long AS Longitude      
 from ScanHistory Where ScannedBy = @Userid      
 And OrgAlias = @OrgCode  
 And  
 (@StartDate Is null or ScannedOn Between @StartDate And @EndDate)  
End      