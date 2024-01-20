import Vue from "vue";
import Vuex from "vuex";
import getAllClients from "@/apis/getAllClients";
import updateClient from "@/apis/updateClient";
import createClient from "@/apis/createClient";
import archiveClient from "@/apis/archiveClient";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    clients: [],
  },
  selectedClient: null,
  mutations: {
    setClients(state, clients) {
      state.clients = clients;
    },
    setSelectedClient(state, client) {
      state.selectedClient = client;
    },
    updateClient(state, updatedClient) {
      const updatedClientID = updatedClient.id;
      console.log("state:", state.clients);
      const index = state.clients.findIndex(
        (client) => client.id === updatedClientID
      );
      if (index !== -1) {
        Vue.set(state.clients, index, updatedClient);
      }
    },
    createClient(state, newClient) {
      newClient.alternativeNumbers;
      state.clients.push(newClient);
    },
    archiveClient(state, client) {
      const clientId = client.id;
      state.clients = state.clients.filter((client) => client.id !== clientId);
    },
  },
  actions: {
    async getAllClients({ commit }) {
      try {
        const clients = await getAllClients();
        commit("setClients", clients);
      } catch (error) {
        console.log(error);
      }
    },

    async createClient({ commit }, newClient) {
      try {
        newClient.alternativeNumbers = await newClient.alternativeNumbers.map(
          (item) => item.number
        );
        const result = await createClient(newClient);
        commit("createClient", result);
      } catch (error) {
        console.log(error);
      }
    },

    async updateClient({ commit }, updatedClient) {
      try {
        updatedClient.alternativeNumbers =
          await updatedClient.alternativeNumbers.map((item) => item.number);
        const result = await updateClient(updatedClient);
        commit("updateClient", result);
      } catch (error) {
        console.log(error);
      }
    },

    async archiveClient({ commit }, client) {
      try {
        const clientId = client.id;
        await archiveClient(clientId);
        commit("archiveClient", client);
      } catch (error) {
        console.log(error);
      }
    },

    selectClient({ commit }, client) {
      commit("setSelectedClient", client);
    },
  },
  modules: {},
});
