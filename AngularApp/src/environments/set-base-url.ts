const fs = require('fs');
const path = require('path');


const proxyConfPath = path.resolve(__dirname, 'proxy.conf.js');
const proxyConfig = require(proxyConfPath);
function setBaseUrl() {
  return proxyConfig.target;
}

module.exports = setBaseUrl;
