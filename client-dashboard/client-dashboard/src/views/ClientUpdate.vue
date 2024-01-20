<template>
  <ClientUpdateForm
    :updateClient="selectedClient"
    :formData="formData"
    @save-and-redirect="saveAndRedirect"
    @archive-client="archiveClient"
    @add-phone-number="addPhoneNumber"
    @remove-phone-number="removePhoneNumber"
  />
</template>

<script>
import ClientUpdateForm from "@/components/ClientUpdateForm.vue";

export default {
  components: {
    ClientUpdateForm,
  },
  data() {
    return {
      formData: {
        id: this.$store.state.selectedClient.id,
        firstName: "",
        lastName: "",
        primaryPhoneNumber: "",
        email: "",
        alternativeNumbers: [],
      },
    };
  },
  computed: {
    selectedClient() {
      return this.$store.state.selectedClient;
    },
  },
  methods: {
    saveAndRedirect(updatedClient) {
      this.$store.dispatch("updateClient", updatedClient);
      this.$router.push("/dashboard");
    },
    archiveClient(archiveClient) {
      this.$store.dispatch("archiveClient", archiveClient);
      this.$router.push("/dashboard");
    },
    addPhoneNumber() {
      this.formData.alternativeNumbers.push({ number: "" });
    },
    removePhoneNumber(index) {
      this.formData.alternativeNumbers.splice(index, 1);
    },
  },
};
</script>

<style scoped lang="scss"></style>
