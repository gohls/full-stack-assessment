<template>
  <div class="container">
    <div class="wrapper">
      <div class="client-info">
        <h3>Client Info</h3>
        <ul>
          <li>{{ displayClient.firstName }} {{ displayClient.lastName }}</li>
          <li>{{ displayClient.primaryPhoneNumber }}</li>
          <li>{{ displayClient.email }}</li>
          <br />
          <b>Alt. Phone #'s</b>
          <li
            v-for="(phone, index) in displayClient.alternativeNumbers"
            :key="index"
          >
            {{ phone }}
          </li>
        </ul>
        <br />
      </div>

      <div class="form">
        <div class="header-section">
          <div></div>
          <h3 class="heading">Update Client</h3>
          <button class="archive-btn" @click="archiveClient(displayClient)">
            <div class="emoji">Archive</div>
          </button>
        </div>
        <form @submit.prevent="saveAndRedirect">
          <p>
            <label for="firstName">First Name</label>
            <input v-model="formData.firstName" type="text" id="firstName" />
          </p>
          <p>
            <label for="lastName">Last Name</label>
            <input v-model="formData.lastName" type="text" id="lastName" />
          </p>
          <p>
            <label for="primaryPhoneNumber">Primary Phone Number</label>
            <input
              v-model="formData.primaryPhoneNumber"
              type="text"
              id="primaryPhoneNumber"
            />
          </p>
          <p>
            <label for="email">Email</label>
            <input v-model="formData.email" type="text" id="email" />
          </p>
          <p
            class="full-width"
            v-for="(alternativeNumber, index) in formData.alternativeNumbers"
            :key="index"
          >
            <label :for="'alternativeNumber' + index"
              >Alternative Number {{ index + 1 }}</label
            >
            <button
              class="remove-btn"
              type="button"
              @click="removeAlternativeNumber(index)"
            >
              ❌
            </button>
            <input
              style="witdh: max-content"
              v-model="alternativeNumber.number"
              type="text"
              :id="'alternativeNumber' + index"
            />
          </p>

          <button class="add-btn" type="button" @click="addAlternativeNumber">
            + Add Alternative Number
          </button>
          <p class="full-width">
            <button>Submit</button>
          </p>
          <!-- <button @click="archiveClient">Archive</button> -->
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    updateClient: Object,
    formData: Object,
  },
  computed: {
    displayClient() {
      return this.updateClient;
    },
  },
  methods: {
    saveAndRedirect() {
      this.$emit("save-and-redirect", this.formData);
    },
    archiveClient(client) {
      this.$emit("archive-client", client);
    },
    addAlternativeNumber() {
      this.$emit("add-phone-number");
    },
    removeAlternativeNumber(index) {
      this.$emit("remove-phone-number", index);
    },
  },
};
</script>

<style scoped lang="scss">
.container {
  max-width: 800px;
  margin: 0 auto;
  /* padding: 20px; */
  border: 1px solid #ddd;
  border-radius: 15px;
}

h3,
ul {
  margin: 0;
}

h3 {
  margin-bottom: 1rem;
}

input,
textarea,
button {
  width: 100%;
  border: 1px solid #000;
}

.wrapper > * {
  padding: 1em;
}
@media (min-width: 700px) {
  .wrapper {
    display: grid;
    grid-template-columns: 1fr 2fr;
  }
}

ul {
  list-style: none;
  padding: 0;
}

.client-info {
  background: linear-gradient(0.35turn, #851e89, #fda847);
  color: #fff;
  text-align: left;
  border-radius: 15px 0px 0px 15px;
}

form {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-gap: 20px;
}

form p {
  margin: 0;
}

.full-width {
  grid-column: 1 / 3;
}

button,
input,
textarea {
  padding: 1em;
  width: 90%;
}

.header-section {
  display: grid;
  grid-template-columns: 33% 33% 35%;
}

.heading {
  margin: 0;
}

.archive-btn {
  justify-self: end;
  background-color: red;
  max-width: fit-content;
}
.archive-btn:hover {
  background-color: #94160b;
  /* color: white; */
  cursor: pointer;
}

button {
  border-radius: 25px;
  border-style: none;
  padding: 5px 10px;
  background: #fda847;
  color: white;
  font-weight: bold;
  transition-duration: 0.4s;
  width: -webkit-fill-available;
}

button:hover {
  background-color: #851e89;
  color: white;
  cursor: pointer;
}

.remove-btn {
  background: transparent;
  color: red;
  margin-top: 10px;
  width: fit-content;
}

.remove-btn:hover {
  background: transparent;
  color: red;
  margin-top: 10px;
  width: fit-content;
}
</style>
