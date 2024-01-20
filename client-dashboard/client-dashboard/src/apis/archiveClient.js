import axios from "axios";

const archiveClient = async (clientId) => {
  try {
    const response = await axios.delete(
      `http://localhost:5000//Client/ArchiveClient/${clientId}`
    );
    return response.data;
  } catch (error) {
    console.error("Error archiving client:", error);
    throw error;
  }
};

export default archiveClient;
