<template>
  <div class="container">
    <div class="header">
      <div class="btns">
        <div class="client-info">Client Information</div>
      </div>
    </div>
    <div class="wrapper">
      <table>
        <thead>
          <tr>
            <th>Client</th>
            <th>Alt. Phone #'s</th>
            <th>Last Updated</th>
            <th></th>
            <!-- Empty column -->
          </tr>
        </thead>
        <tbody>
          <tr v-for="(client, index) in tableData" :key="index">
            <td>
              <b>{{ client.firstName }} {{ client.lastName }}</b>
              <br />
              <span
                ><div class="emoji">✉️</div>
                {{ client.email }}</span
              >
            </td>
            <td>
              <div class="emoji">📞&nbsp;</div>
              <b>{{ client.primaryPhoneNumber }}</b>
              <div
                v-for="(phone, index) in client.alternativeNumbers"
                :key="index"
              >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{{ phone }}
              </div>
            </td>
            <td>
              {{ formattedDate(client.lastUpdated) }}
            </td>
            <td>
              <button class="update-btn" @click="openClientUpdate(client)">
                Update
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    tableData: {
      type: Array,
      default: () => [],
    },
  },
  methods: {
    openClientUpdate(clientData) {
      const clientId = clientData.id;
      this.$store.dispatch("selectClient", clientData);
      console.log("client id:", clientId);
      this.$router.push(`/client/update/${clientId}`);
    },
    archiveClient(clientData) {
      const clientId = clientData.id;
      this.$store.dispatch("archiveClient", clientId);
    },
    formattedDate(date) {
      const d = new Date(date);
      return d.toLocaleDateString();
    },
  },
  // data() {
  //   return {
  //     searchQuery: "",
  //   };
  // },
};
</script>

<style scoped lang="scss">
.container {
  max-width: 800px;
  max-height: 85vh;
  margin: 0 auto;
  border: 1px solid #ddd;
  border-radius: 15px;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: linear-gradient(0.25turn, #851e89, #fda847);
  border-radius: 15px 15px 0 0;
  padding: 16px;
  font-size: 20px;

  .client-info {
    color: white;
    font-weight: bold;
  }

  .search-bar {
    width: 40%;

    input {
      width: 90%;
      border-radius: 15px;
      border-style: none;
    }

    input[type="text"] {
      padding: 5px 15px;
    }
  }
}

.wrapper {
  width: 100%;
  max-height: 75vh;
  overflow: auto;

  .emoji {
    display: inline;
    color: transparent;
    text-shadow: 0 0 0 #fda847;
  }
}

table {
  width: 100%;
  text-align: center;
  border-collapse: separate;
  border-spacing: 0;
  text-align: left;
  vertical-align: top;
  padding: 0 12px 12px 12px;

  .update-btn {
    border-radius: 25px;
    border-style: none;
    padding: 5px 10px;
    background: #fda847;
    color: white;
    font-weight: bold;
    transition-duration: 0.4s;
  }

  .btns {
    display: flex;
    justify-content: space-between;
  }
  .update-btn:hover {
    background-color: #851e89;
    color: white;
    cursor: pointer;
  }
}

table th {
  border-bottom: 2px solid #ddd;
}

table td {
  border-bottom: 1px solid #ddd;
  text-align: left;
  vertical-align: top;
  padding: 12px;
}

table thead th {
  position: sticky;
  top: 0;
  background-color: white;
  padding: 12px;
}
</style>
