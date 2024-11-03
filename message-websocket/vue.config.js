const { defineConfig } = require('@vue/cli-service');

module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    proxy: {
      '/api': {
        // target: 'http://localhost:5121',
        // I'm not sure if this works vv
        target: process.env.VUE_APP_API_URL,
        changeOrigin: true,
      },
    },
  },
});
