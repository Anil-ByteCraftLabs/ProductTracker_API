Create Procedure [dbo].[usp_SaveProduct] (
  @ProductId int, 
  @Name Varchar(100), 
  @Description varchar(500), 
  @Quantity int, 
  @FssaiCode nvarchar(50), 
  @Category int, 
  @Weight Int, 
  @Price int, 
  @PriceStartDate Datetime = Null, 
  @IsActive bit, 
  @CreatedBy nvarchar(450),
  @PlantId int
) As

Begin If (@ProductId > 0) Begin 
Update 
  ProductData 
set 
  [ProductName] = @Name, 
  [Description] = @Description, 
  [Quantity ] = @Quantity, 
  [FssaiCode] = @FssaiCode, 
  [IsActive] = @IsActive, 
  CategoryId = @Category, 
  WeightId = @Weight, 
  UpdatedBy = @CreatedBy, 
  UpdatedOn = GETDATE() ,
  PlantId= @PlantId

Where 
  Id = @ProductId 
 End 
 Else 
 Begin INSERT INTO [dbo].ProductData (
    [ProductName], [Description], [Quantity], 
    [FssaiCode], [IsActive], CategoryId, 
    WeightId, [CreatedBy], [CreatedOn], PlantId
  ) 
VALUES 
  (
    @Name, 
    @Description, 
    @Quantity, 
    @FssaiCode, 
    1, 
    @Category, 
    @Weight, 
    @CreatedBy, 
    Getdate(),
	@PlantId

  ) 
  Declare @NewProductId int = @@Identity Insert into ProductPrice (
    ProductId, Price, StartDate, EndDate, 
    IsActive, CreatedBy, CreatedOn
  ) 
values 
  (
    @NewProductId, 
    @Price, 
    IIF(
      @PriceStartDate is null, 
      Getdate(), 
      @PriceStartDate
    ), 
    '2050-01-01', 
    1, 
    @CreatedBy, 
    GETDATE()
  ) End End
