// import { StrictMode } from 'react'
import { createRoot } from "react-dom/client";
import "./index.css";
// import App from './App.tsx'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import SubmitHandlers from "./page/Dat/SubmitHandlers.tsx";
import { Provider } from "react-redux";
import { store } from "./redux/store.tsx";
import Layout from "./components/layout.tsx";
import Trangchu from "./page/Trangchu.tsx";
import SubmitHandlerHouses from "./page/Nha/SubmitHandlersNha.tsx";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "/home",
        element: <SubmitHandlers />,
      },
      {
        path: "/",
        element: <Layout />,
        children: [
          {
            path: "/nha",
            element: <SubmitHandlerHouses />,
          },
        ],
      },

      {
        path: "/trangchu",
        element: <Trangchu />,
      },
    ],
  },
]);
createRoot(document.getElementById("root")!).render(
  <Provider store={store}>
    <RouterProvider router={router} />
  </Provider>
);
