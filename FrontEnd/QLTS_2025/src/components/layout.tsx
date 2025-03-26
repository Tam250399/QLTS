import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../redux/store";
import { showToast } from "../helpers/myHelper";
import { clearToast } from "../redux/toastLice";
import { useEffect } from "react";
import { Outlet } from "react-router-dom";

function Layout() {
  const { message, type } = useSelector((state: RootState) => state.toast);
  const dispatch = useDispatch();

  useEffect(() => {
    showToast(message, type);
    dispatch(clearToast());
  }, [message, type]);

  return (
    <div>
      <Outlet />
    </div>
  );
}

export default Layout;
