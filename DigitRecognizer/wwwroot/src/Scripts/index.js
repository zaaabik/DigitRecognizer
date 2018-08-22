import Vue from 'vue';
import routerConfig from '@/Scripts/router';
import Index from './Components/Index.vue';
import VueRouter from 'vue-router';
import BootstrapVue from 'bootstrap-vue';

import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

const router = new VueRouter(
    routerConfig
);

Vue.use(VueRouter);
Vue.use(BootstrapVue);

new Vue({
    router,
    el: '#index',
    render: h => h(Index)
});