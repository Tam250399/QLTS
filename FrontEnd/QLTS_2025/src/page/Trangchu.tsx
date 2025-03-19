import { Grid, Card, CardContent, Typography, Box } from "@mui/material";
import PublicIcon from "@mui/icons-material/Public";
import HomeIcon from "@mui/icons-material/Home";
import AnalyticsIcon from "@mui/icons-material/Analytics";
import DirectionsCarIcon from "@mui/icons-material/DirectionsCar";
import DirectionsBoatIcon from "@mui/icons-material/DirectionsBoat";
import ComputerIcon from "@mui/icons-material/Computer";
import ParkIcon from "@mui/icons-material/Park";
import CubeIcon from "@mui/icons-material/Park"; // Không có icon Cube trực tiếp, bạn có thể dùng icon khác hoặc tự tạo
import LinkIcon from "@mui/icons-material/Link";
import SettingsIcon from "@mui/icons-material/Settings";
import { useNavigate } from "react-router-dom";

const Trangchu = () => {
  const menuItems = [
    { title: "Đất", icon: <PublicIcon /> },
    { title: "Nhà", icon: <HomeIcon /> },
    { title: "Vật kiến trúc", icon: <AnalyticsIcon /> },
    { title: "Ô tô", icon: <DirectionsCarIcon /> },
    { title: "PTVT khác", icon: <DirectionsBoatIcon /> },
    {
      title: "Máy móc thiết bị",
      icon: <ComputerIcon />,
      link: "/home",
    },
    { title: "Cây lâu năm, svlv", icon: <ParkIcon /> },
    {
      title: "TSCĐ hữu hình khác",
      icon: <CubeIcon />,
      link: "/tscd-huu-hinh-khac",
    },
    { title: "TSCĐ vô hình", icon: <LinkIcon /> },
    {
      title: "Tài sản quản lý như TSCĐ",
      icon: <SettingsIcon />,
    },
  ];

  const navigate = useNavigate();

  const handleCardClick = (title) => {
    switch (title) {
      case "Đất":
        navigate("/home");
        break;
      case "Nhà":
        navigate("/nha");
        break;
      default:
        navigate("/layout");
        break;
    }
  };
  return (
    <Box sx={{ padding: 2 }}>
      <Grid container spacing={2}>
        {menuItems.map((item, index) => (
          <Grid item xs={12} sm={6} md={3} key={index}>
            <Card
              sx={{
                display: "flex",
                alignItems: "center",
                padding: 2,
                backgroundColor: "#fff",
                border: "1px solid #ddd",
                "&:hover": {
                  backgroundColor: "#F5F5F5",
                },
              }}
              onClick={() => handleCardClick(item.title)}
              style={{ cursor: "pointer" }}
            >
              <Box
                sx={{
                  marginRight: 2,
                  color: "#1976d2",
                }}
              >
                {item.icon}
              </Box>
              <CardContent sx={{ padding: 0 }}>
                <Typography variant="body1">{item.title}</Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};

export default Trangchu;
