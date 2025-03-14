import { requestAuth } from "../config/axiosConfig";
export const fetchCountry = async () => {
  try {
    const response = await requestAuth({
      url: `/v3.1/all`,
      method: "GET",
    });
    return response;
  } catch (error) {
    console.error("Error fetching countries:", error);
    return [];
  }
};
