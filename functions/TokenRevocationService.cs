using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.functions
{
  public class TokenRevocationService
  {
    private static readonly List<string> _revokedTokens = new List<string>();

    public static void RevokeToken(string token)
    {
      // Add the token to the list of revoked tokens
      _revokedTokens.Add(token);
    }

    public bool IsTokenRevoked(string token)
    {
      // Check if the token is in the list of revoked tokens
      return _revokedTokens.Contains(token);
    }
  }
}
