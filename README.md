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

* Instalar o [.Net Core 2.0 SDK](https://www.microsoft.com/net/download/core), em seguida use os seguintes comandos no terminal para clonar e executar o projeto:
* mkdir c:\dev\multi-credit-card

```
https://github.com/ddrsdiego/multi-credit-card.git
```

## Endpoints

### Autentica usuário
| Campo     | Tipo     |Descrição
| --------- | -------- |------------- |
| email     | String   | Email do usuário
| password  | String   | Senha do usuário

```
### POST
{
	"email":"lararibeirocavalcanti@jourrapide.com",
	"password":"1234mudar"
}
```

### Registrar um novo usuário
| Campo           | Tipo          |Descrição
| -------------   | ------------- |------------- |
| userName        | String        | Nome do usuário
| documentNumber  | decimal       | Número do documento do usuário
| email           | String        | Email do usuário
| password        | String        | Senha do usuário

```
### POST
http://localhost:60091/api/users/
{
	"userName":"Lara Ribeiro Cavalcanti",
	"documentNumber":10883620448,
	"email":"lararibeirocavalcanti@jourrapide.com",
	"password":"1234mudar"
}
```

### Adicionar cartões de crédito a carteira do cliente

| Campo          | Tipo   |Descrição
| -------------  | ------ |------------- |
| Authorization  | String |Informar como "Bearer Token"
| Content-Type   | String |application/json

| Campo            | Tipo     |Descrição
| -------------    | ---------|------------- |
| creditCardType   | string   | Bandeira do Cartão ( 1 = Visa, 2 = Master Card, 3 = AmericanExpress )
| creditCardNumber | decimal  | Número do Cartão de Crédito
| cvv              | string   | Código de Segurança
| expirationDate   | string   | Data de Expiração do Cartão de Crédito
| printedName      | string   | Nome Impresso no Cartão de Crédito
| payDay           | string   | Dia do pagamento da fatura do Cartão de Crédito
| creditLimit      | decimal  | Valor do Limite de Crédito do Cartão

```
### POST
http://localhost:60091/api/credit-cards
{
	"creditCardType":2,
	"creditCardNumber":5350172366189464,
	"cvv":"746",	
	"expirationDate":"02/19",	
	"printedName":"LARA CAVALCANTI",
	"payDay":15,
	"creditLimit":1750
}
```
### Requisitar compras.

| Campo          | Tipo   |Descrição
| -------------  | ------ |------------- |
| Authorization  | String |Informar como "Bearer Token"
| Content-Type   | String |application/json

| Campo            | Tipo     |Descrição
| -------------    | ---------|------------- |
| amountValue      | decimal  | Valor da Compra a ser executada

```
### POST
{
	"amountValue":1450
}
```