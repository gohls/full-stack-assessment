<template>
  <div class="container">
    <div class="wrapper">
      <div class="client-info">
        <h3>Review</h3>
        <ul>
          <li>{{ newClient.firstName }} {{ newClient.lastName }}</li>
          <li>{{ newClient.primaryPhoneNumber }}</li>
          <li>{{ newClient.email }}</li>
          <br />
          <b>Alt. Phone #'s</b>
          <li
            v-for="(phone, index) in newClient.alternativeNumbers"
            :key="index"
          >
            {{ phone.number }}
          </li>
        </ul>
        <br />
      </div>

      <div class="form">
        <h3>Add Client</h3>
        <form @submit.prevent="addAndRedirect">
          <p>
            <label for="firstName">First Name</label>
            <input v-model="newClient.firstName" type="text" id="firstName" />
          </p>
          <p>
            <label for="lastName">Last Name</label>
            <input v-model="newClient.lastName" type="text" id="lastName" />
          </p>
          <p>
            <label for="primaryPhoneNumber">Primary Phone Number</label>
            <input
              v-model="newClient.primaryPhoneNumber"
              type="text"
              id="primaryPhoneNumber"
            />
          </p>
          <p>
            <label for="email">Email</label>
            <input v-model="newClient.email" type="text" id="email" />
          </p>
          <p
            class="full-width"
            v-for="(alternativeNumber, index) in newClient.alternativeNumbers"
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
              v-model="alternativeNumber.number"
              type="text"
              :id="'alternativeNumber' + index"
            />
          </p>

          <button class="add-btn" type="button" @click="addAlternativeNumber">
            + Add Alternative Number
          </button>

          <p class="full-width">
            <button type="submit">Submit</button>
          </p>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      newClient: {
        firstName: "",
        lastName: "",
        primaryPhoneNumber: "",
        email: "",
        archive: false,
        alternativeNumbers: [],
      },
    };
  },
  methods: {
    addAndRedirect() {
      this.$emit("add-and-redirect", this.newClient);
    },
    addAlternativeNumber() {
      this.newClient.alternativeNumbers.push({ number: "" });
    },
    removeAlternativeNumber(index) {
      this.newClient.alternativeNumbers.splice(index, 1);
    },
  },
};
</script>

<style scoped lang="scss">
.container {
  max-width: 800px;
  margin: 0 auto;
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
  width: -webkit-fill-available;
}

button {
  border-radius: 25px;
  border-style: none;
  padding: 5px 10px;
  background: #fda847;
  color: white;
  font-weight: bold;
  transition-duration: 0.4s;
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

.add-btn {
  grid-column: 1 / 3;
  width: max-content;
  margin: 0 auto;
}
</style>
