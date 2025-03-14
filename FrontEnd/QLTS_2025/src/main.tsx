import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import {RouterProvider,createBrowserRouter}from "react-router-dom";
import Home from "./page/Home.tsx"
import { Provider } from 'react-redux';
import { store } from './redux/store.tsx';
const router = createBrowserRouter ([
  // {
  //   path: "/",
  //   element: <Login />,
  //   errorElement: <NotFound />, // Xử lý lỗi
  // },
  {
    path:"/home",
    element :<Home />,
  },
 
  
])
createRoot(document.getElementById('root')!).render(
<Provider store={store} >

<RouterProvider router={router} />
</Provider>
)
