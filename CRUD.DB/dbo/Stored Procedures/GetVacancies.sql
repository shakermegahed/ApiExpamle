
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetVacancies] 
	
AS
BEGIN
	SELECT *
	from 
Vacaion 
inner join Department on Vacaion.Department = Department.id

END