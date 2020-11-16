IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[FI_SP_DelBeneficiario]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [FI_SP_DelCliente]
END
GO

CREATE PROC FI_SP_DelBeneficiario
	@ID BIGINT
AS
BEGIN
	DELETE BENEFICIARIOS WHERE ID = @ID
END