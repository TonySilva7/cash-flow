using System.IdentityModel.Tokens.Jwt;  // Biblioteca para manipulação de tokens JWT
using System.Security.Claims;  // Para lidar com declarações (claims) em tokens
using System.Text;  // Para conversão de strings em bytes (usado na chave de segurança)
using CashFlow.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;  // Classes e métodos para criptografia de tokens

namespace CashFlow.Infrastructure.Security.Tokens.Access.Generator;

// Implementação da interface IAccessTokenGenerator para gerar tokens JWT
public class JwtTokenGenerator(uint expirationTimeInMinutes, string signingKey) : JwtTokenHanlder, IAccessTokenGenerator
{
    // Método público para gerar um token JWT, recebendo o ID do usuário como identificador
    public string Generate(Guid userIdentifier)
    {
        var claimIdentifier = new Claim(ClaimTypes.Sid, userIdentifier.ToString());
        List<Claim> claims = [claimIdentifier];

        // Descrição do token, onde definimos sua expiração, credenciais e claims
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Define a expiração do token, somando os minutos passados no construtor ao horário atual (em UTC)
            Expires = DateTime.UtcNow.AddMinutes(expirationTimeInMinutes),

            // Define o algoritmo de assinatura do token, neste caso, HMAC SHA256 com uma chave simétrica
            SigningCredentials = new SigningCredentials(
                CreateSecurityKey(signingKey),  // Método privado para gerar a chave simétrica
                SecurityAlgorithms.HmacSha256Signature  // Algoritmo de assinatura HMAC com SHA256
            ),

            // Define um array "claims" do token, neste caso, apenas o ID do usuário (NameIdentifier)
            Subject = new ClaimsIdentity(claims) // Armazena o ID do usuário
        };

        // Handler para criar e manipular o token JWT
        var tokenHandler = new JwtSecurityTokenHandler();

        // Cria o token baseado no tokenDescriptor definido acima
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Gera o token JWT como uma string compactada
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
