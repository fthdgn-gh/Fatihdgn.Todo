import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.fatihdgn.todo',
  appName: 'Todo',
  webDir: 'dist/fatihdgn.todo.web',
  server: {
    androidScheme: 'https'
  }
};

export default config;
