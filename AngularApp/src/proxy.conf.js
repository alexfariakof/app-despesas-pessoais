const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:42535';

const PROXY_CONFIG = [
  {
    "/api": {
      target: target,
      "context": [
        "/controleacesso",
        "/categoria",
        "/despesa",
        "/receita",
        "/lancamento",
        "/graficos",
        "/saldo",
        "/usuario",
      ],
      secure: false,
      changeOrigin: true,
      headers: {
        Connection: 'Keep-Alive'
      }
    }
  }
]

module.exports = PROXY_CONFIG;

