CREATE PROCEDURE [dbo].[usp_GetBatchAllCoupons]   
 (  
  @BatchId int
 )   
AS   
BEGIN   
  

Select Cou.Id,Cou.UniqueId,Cou.BatchId, Bat.  
[Name] 'BatchName',  
       Cou.OrgAliasName,  
       Cou.ParentCouponId,  
       Cou.IsScanned,  
       Cou.ScannedDate,  
       Cou.ScannedBy,  
       Cou.IsActive,  
       Cou.CreatedBy,  
       Cou.CreatedOn,  
       Cou.UpdatedBy,  
       Cou.UpdatedOn,  
    Cou.IsPrinted,  
    Cou.ScanCount  
FROM CouponsData Cou  
JOIN BatchData Bat ON Cou.BatchId = Bat.Id  
WHERE Cou.BatchId = 1  
  AND (IsPrinted Is null Or IsPrinted = 0  )
 END
