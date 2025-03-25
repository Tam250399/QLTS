import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { ToastType } from "../context/ToastContext";
export interface ToastState {
  message: string;
  type: ToastType;
}

const initialState: ToastState = {
  message: "",
  type: null,
};

export const ToastSlice = createSlice({
  name: "toast",
  initialState,
  reducers: {
    setToast: (
      state,
      action: PayloadAction<{ message: string; type: ToastType }>
    ) => {
      state.message = action.payload.message;
      state.type = action.payload.type;
    },

    clearToast: (state) => {
      state.message = "";
      state.type = null;
    },
  },
});

// Action creators are generated for each case reducer function
export const { setToast, clearToast } = ToastSlice.actions;

export default ToastSlice.reducer;
