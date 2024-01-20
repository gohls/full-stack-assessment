<template>
  <ClientAddForm :newClient="newClient" @add-and-redirect="addAndRedirect" />
</template>

<script>
import ClientAddForm from "@/components/ClientAddForm.vue";

export default {
  components: {
    ClientAddForm,
  },
  data() {
    return {
      newClient: {
        firstName: "",
        lastName: "",
        email: "",
        primaryPhoneNumber: "",
        alternativeNumbers: [],
      },
    };
  },
  methods: {
    addAndRedirect(newClient) {
      this.$store.dispatch("createClient", newClient);
      this.$router.push("/dashboard");
    },
  },
};
</script>

<style scoped lang="scss"></style>
