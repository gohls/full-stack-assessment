import axios from "axios";

const updateClient = async (updatedClient) => {
  try {
    const response = await axios.put(
      `http://localhost:5000/Client/UpdateClient`,
      updatedClient
    );
    return response.data;
  } catch (error) {
    console.error("Error updating client:", error);
    throw error;
  }
};

export default updateClient;
