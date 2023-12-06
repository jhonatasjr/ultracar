const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function (app) {
  app.use(
    '/api', // O prefixo da sua API
    createProxyMiddleware({
      target: 'https://localhost:7267',
      changeOrigin: true,
    })
  );
};