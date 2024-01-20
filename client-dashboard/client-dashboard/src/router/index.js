import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "home",
    component: Home,
  },
  {
    path: "/dashboard",
    name: "dashboard",
    component: () => import("../views/ClientDashboard.vue"),
  },
  {
    path: "/client/add",
    name: "add",
    component: () => import("../views/ClientAdd.vue"),
  },
  {
    path: "/client/update/:clientId",
    name: "client",
    component: () => import("../views/ClientUpdate.vue"),
  },
  {
    path: "/about",
    name: "about",
    component: () => import("../views/About.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
