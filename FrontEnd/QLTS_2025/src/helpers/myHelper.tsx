import { toast } from "react-toastify";

import { ToastType } from "../context/ToastContext";

export const showNotify = (
  message: string,
  type: ToastType,
  setMessage: (message: string, type?: ToastType) => void
) => {
  if (message) {
    switch (type) {
      case "success":
        toast.success(message);
        break;
      case "error":
        toast.error(message);
        break;
      case "warning":
        toast.warning(message);
        break;
      default:
        break;
    }
    setMessage("", null);
  }
};

export const showToast = (message: string, type: ToastType) => {
  if (message) {
    switch (type) {
      case "success":
        toast.success(message);
        break;
      case "error":
        toast.error(message);
        break;
      case "warning":
        toast.warning(message);
        break;
      default:
        break;
    }
  }
};
