import { createApp } from 'vue';
import vuetify from './Plugins/vuetify';
import App from './Components/App.vue';
import router from './router';


const app = createApp(App);
app.use(router);
app.use(vuetify)
app.mount('#app');
