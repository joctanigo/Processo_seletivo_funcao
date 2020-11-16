IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[FI_SP_AltBeneficiario]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [FI_SP_AltBeneficiario]
END
GO

CREATE PROC FI_SP_AltBeneficiario
    @NOME          VARCHAR (50) ,
    @IDCLIENTE     BIGINT ,
	@CPF		   VARCHAR (15),
	@Id           BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS 
	SET 
		NOME = @NOME, 
		IDCLIENTE = @IDCLIENTE, 
		CPF = @CPF
	WHERE Id = @Id
END