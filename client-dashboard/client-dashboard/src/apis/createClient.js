import axios from "axios";

const createClient = async (newClient) => {
  try {
    const response = await axios.post(
      "http://localhost:5000/Client/CreateClient",
      newClient
    );
    return response.data;
  } catch (error) {
    console.error("Error creating client:", error);
    throw error;
  }
};

export default createClient;
