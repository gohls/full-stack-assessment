<template>
  <div class="dashboard">
    <ClientListTable :tableData="clients" />
  </div>
</template>

<script>
import { mapState } from "vuex";
import ClientListTable from "@/components/ClientListTable.vue";

export default {
  components: {
    ClientListTable,
  },
  computed: {
    ...mapState(["clients"]), // Map the 'clients' state from the Vuex store
  },
  created() {
    // Dispatch the getAllClients action when the Dashboard view is created
    this.$store.dispatch("getAllClients");
  },
  watch: {
    clients: {
      handler(newClients) {
        console.log("Clients state changed:", newClients);
      },
      deep: true,
    },
  },
};
</script>

<style scoped>
.dashboard {
  display: flex;
  align-items: center;
  justify-content: center;
  /* height: 100vh; Full height of the screen */
}

.dashboard > .container {
  width: 66.67%; /* 2/3 of the screen width */
}
</style>
