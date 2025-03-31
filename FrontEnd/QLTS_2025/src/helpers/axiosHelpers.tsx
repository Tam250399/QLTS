import axios from "axios";
import { toast } from "react-toastify";

const handleAxiosError = (error: unknown): void => {
  if (axios.isAxiosError(error)) {
    console.log("error", error);

    if (error.response && error.response.data) {
      let errorMessages: string[] = [];

      if (error.response.data.validationErrors?.length > 0) {
        errorMessages = error.response.data.validationErrors.map(
          (errorMsg: string) =>
            errorMsg.includes("@") ? errorMsg.split("@")[1] : errorMsg
        );
      } else if (error.response.data.message) {
        const trimmedMsg = error.response.data.message.includes("@")
          ? error.response.data.message.split("@")[1]
          : error.response.data.message;
        errorMessages.push(trimmedMsg);
      }

      if (errorMessages.length > 0) {
        toast.error(
          <div style={{ textAlign: "left" }}>
            {errorMessages.map((msg, index) => (
              <div
                key={index}
                style={{
                  display: "flex",
                  alignItems: "center",
                  marginBottom: "4px",
                }}
              >
                <span style={{ marginRight: "8px" }}>•</span>
                <span>{msg}</span>
              </div>
            ))}
          </div>
        );
      } else {
        toast.error("Network Error");
      }
    } else {
      toast.error("Network Error");
    }
  } else {
    toast.error("Đã xảy ra lỗi không xác định.");
  }
};

export { handleAxiosError };
