# MultiCreditCard

Projeto desenvolvido como solução do teste da Stone

O projeto MultiCreditCard foi desenvolvido com as seguintes tecnologias.

* ASP.NET Core 2.0
* [Log4Net] para geração de logs (https://github.com/apache/logging-log4net/)
* [Fluent Validation] para utitizar o padrão fail-fast (https://github.com/JeremySkinner/FluentValidation)
* [MediatR] com a bibilioteca https://github.com/jbogard/MediatR)
* [PaperTrail] para visualização dos logs (https://papertrailapp.com) 
* [SQL Server] Banco de dados.
* [JWT] para autenticação de usuário baseada em token (https://tools.ietf.org/html/rfc7519)


## Como rodar esse projeto localmente

Primeiramente será necessário instalar o [.Net Core 2.0 SDK](https://www.microsoft.com/net/download/core), em seguida use os seguintes comandos no terminal para clonar e executar o projeto:

```
https://github.com/ddrsdiego/multi-credit-card.git
```


## Endpoints
### Registrar um novo usuário

{
	"userName":"Lara Ribeiro Cavalcanti",
	"documentNumber":10883620448,
	"email":"lararibeirocavalcanti@jourrapide.com",
	"password":"1234mudar"
}


{
	"creditCardType":2,
	"creditCardNumber":5350172366189464,
	"cvv":"746",	
	"expirationDate":"02/19",	
	"maturityDate":"02/19",	
	"printedName":"LARA CAVALCANTI",
	"payDay":15,
	"creditLimit":1750
}


{
	"amountValue":1450
}