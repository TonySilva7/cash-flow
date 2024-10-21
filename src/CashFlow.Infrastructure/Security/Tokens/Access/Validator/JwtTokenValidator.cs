using System.IdentityModel.Tokens.Jwt;  // Biblioteca usada para manipulação de tokens JWT
using System.Security.Claims;  // Manipulação de Claims, que são dados associados ao token
using CashFlow.Domain.Security.Tokens;  // Namespace do projeto, contendo abstrações de tokens
using Microsoft.IdentityModel.Tokens;  // Ferramentas de segurança para manipulação de tokens e chaves de assinatura

namespace CashFlow.Infrastructure.Security.Tokens.Access.Validator
{
    // Classe para validar tokens JWT, herdando de JwtTokenHandler e implementando IAccessTokenValidator
    public class JwtTokenValidator(string signingKey) : JwtTokenHanlder, IAccessTokenValidator
    {
        // Método público que valida o token JWT e retorna o identificador do usuário (Guid)
        public Guid ValidateAndGetUserIdentifier(string accessToken)
        {
            // Parâmetros de validação do token, usados pelo validador de tokens JWT
            var validationParameter = new TokenValidationParameters
            {
                // Não valida o público (audience) do token
                ValidateAudience = false,

                // Não valida o emissor (issuer) do token
                ValidateIssuer = false,

                // Descomente para validar o tempo de vida do token e a chave de assinatura, se necessário
                // ValidateLifetime = true,
                // ValidateIssuerSigningKey = true,

                // Define a chave de assinatura usada para validar a autenticidade do token
                IssuerSigningKey = CreateSecurityKey(signingKey),  // Usa o método CreateSecurityKey para gerar a chave simétrica

                // Define o tempo máximo de discrepância permitido entre o horário do token e o horário do servidor
                // Aqui, ClockSkew é definido como zero, ou seja, não há tolerância para diferenças de tempo
                ClockSkew = TimeSpan.Zero
            };

            // Cria um handler para manipular o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Valida o token usando os parâmetros de validação configurados
            // O método ValidateToken retorna um principal (entidade associada ao token)
            // e também o securityToken (opcional) que representa o token de segurança
            var principal = tokenHandler.ValidateToken(accessToken, validationParameter, out var securityToken);

            // Busca a claim que contém o ID do usuário (assumido estar no ClaimTypes.Sid)
            var userIdentifier = principal.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            // Converte o ID do usuário de string para Guid e o retorna
            return Guid.Parse(userIdentifier);
        }
    }
}
