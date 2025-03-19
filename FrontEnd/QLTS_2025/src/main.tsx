// import { StrictMode } from 'react'
import { createRoot } from "react-dom/client";
import "./index.css";
// import App from './App.tsx'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import SubmitHandlers from "./page/Dat/SubmitHandlers.tsx";
import { Provider } from "react-redux";
import { store } from "./redux/store.tsx";
import Layout from "./components/layout.tsx";

const router = createBrowserRouter([
  // {
  //   path: "/",
  //   element: <Login />,
  //   errorElement: <NotFound />, // Xử lý lỗi
  // },
  // {
  //   path:"/layout",
  //   element :<Layout />,
  // },

  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "/home",
        element: <SubmitHandlers />,
      },
    ],
  },
]);
createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <RouterProvider router={router} />
  </Provider>
);
