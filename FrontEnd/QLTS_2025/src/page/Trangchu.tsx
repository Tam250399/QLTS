import { useEffect, useState } from "react";
import "../App.css";
import {
  FaGlobe,
  FaHome,
  FaChartLine,
  FaCar,
  FaShip,
  FaDesktop,
  FaTree,
  FaCube,
  FaCircleNotch,
  FaSlidersH,
} from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { GetListIdLoaiHinhTS } from "../service/LoaiHinhTaiSanService";
import { LoaiHinhTSId } from "../validateform/loaihinhtaisanid";

const Trangchu = () => {
  const navigate = useNavigate();
  const [listIdLhts, setListIdLhts] = useState<LoaiHinhTSId[]>([]);
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await GetListIdLoaiHinhTS();
        setListIdLhts(response);
      } catch (error) {
        console.error("Lỗi khi tải dữ liệu:", error);
      }
    };

    fetchData();
  }, []);

  const cards = [
    { icon: <FaGlobe />, text: "Đất", title: "Đất", color: "#00C4B4" },
    { icon: <FaHome />, text: "Nhà", title: "Nhà", color: "#1E90FF" },
    {
      icon: <FaChartLine />,
      text: "Vật kiến trúc",
      title: "Vật kiến trúc",
      color: "#1E90FF",
    },
    { icon: <FaCar />, text: "Ô tô", title: "Xe ôtô", color: "#FF4500" },
    {
      icon: <FaShip />,
      text: "PTVT khác",
      title: "Phương tiện vận tải khác (ngoài xe ô tô)",
      color: "#1E90FF",
    },
    {
      icon: <FaDesktop />,
      text: "Máy móc thiết bị",
      title: "Máy móc, thiết bị",
      color: "#FFD700",
    },
    {
      icon: <FaTree />,
      text: "Cây lâu năm, svlv",
      title: "Cây lâu năm, súc vật làm việc",
      color: "#32CD32",
    },
    {
      icon: <FaCube />,
      text: "TSCĐ hữu hình khác",
      title: "TSCĐ hữu hình khác",
      color: "#808080",
    },
    {
      icon: <FaCircleNotch />,
      text: "TSCĐ vô hình",
      title: "TSCĐ vô hình",
      color: "#FF4500",
    },
    {
      icon: <FaSlidersH />,
      text: "Tài sản quản lý như TSCĐ",
      title: "TSCĐ đặc thù",
      color: "#1E90FF",
    },
  ];

  const handleCardClick = (title: string) => {
    const routes: Record<string, string> = {
      "Đất": "/home",
      "Nhà": "/nha",
      "Vật kiến trúc": "/taisanvkt",
    };

    const foundItem = Array.isArray(listIdLhts)
      ? listIdLhts.find((item) => item.TEN === title)
      : undefined;

    const id = foundItem ? foundItem.LOAI_HINH_TAI_SAN_ID : null;
    const path = routes[title] || "/trangchu";

    navigate(path, { state: id ? { id } : undefined });
  };

  return (
    <div className="container ">
      <h2 className="title pb-10">Nhập số dư đầu kỳ - Mới chọn nhóm tài sản</h2>
      <div className="grid">
        {cards.map((card, index) => (
          <div
            key={index}
            className="card cursor-pointer"
            onClick={() => handleCardClick(card.title)}
          >
            <div className="icon" style={{ color: card.color }}>
              {card.icon}
            </div>
            <p>{card.text}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Trangchu;
