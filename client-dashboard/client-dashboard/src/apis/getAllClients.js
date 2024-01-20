import axios from "axios";

const getAllClients = async () => {
  try {
    const response = await axios.get(
      "http://localhost:5000/Client/GetAllClients"
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching all clients:", error);
    throw error;
  }
};

export default getAllClients;
