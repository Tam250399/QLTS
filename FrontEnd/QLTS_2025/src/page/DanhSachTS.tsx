import { useState } from "react";
import {
  Box,
  TextField,
  Select,
  MenuItem,
  Button,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Checkbox,
} from "@mui/material";

import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import VisibilityIcon from "@mui/icons-material/Visibility";
import AddIcon from "@mui/icons-material/Add";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";

const AssetList = () => {
  const [keyword, setKeyword] = useState("");
  const [assetType, setAssetType] = useState("");
  const [department, setDepartment] = useState("");

  const [page] = useState(0);
  const [rowsPerPage] = useState(10);

  // Data fake
  const rows = [
    {
      id: 1,
      code: "011008-203-901685",
      name: "Nhà số 3 (C), SG2 Lê Thạch",
      type: "Nhà cấp II",
      value: "6,068,825,000",
      department: "Nhà cấp II",
      date: "13/03/2019",
    },
    {
      id: 2,
      code: "011008-204-901689",
      name: "Nhà số 8 (M) - SG2 Lê Thạch",
      type: "Nhà cấp III",
      value: "21,852,000",
      department: "Nhà cấp III",
      date: "01/01/1993",
    },
    {
      id: 3,
      code: "011008-204-901671",
      name: "Nhà số 9 (N), SG2 Lê Thạch",
      type: "Nhà cấp III",
      value: "10,926,000",
      department: "Nhà cấp III",
      date: "01/01/1992",
    },
    {
      id: 4,
      code: "011008-203-901686",
      name: "Nhà số 4 (D), SG2 Lê Thạch",
      type: "Nhà cấp II",
      value: "874,676,545",
      department: "Nhà cấp II",
      date: "01/01/1982",
    },
    {
      id: 5,
      code: "011008-204-901670",
      name: "Nhà số 7 (H), SG2 Lê Thạch",
      type: "Nhà cấp III",
      value: "25,092,000",
      department: "Nhà cấp III",
      date: "01/01/1978",
    },
    {
      id: 6,
      code: "011008-204-901684",
      name: "Nhà số 2 (B), SG2 Lê Thạch",
      type: "Nhà cấp I",
      value: "11,334,281,095",
      department: "Nhà cấp I",
      date: "01/01/1975",
    },
    {
      id: 7,
      code: "011008-204-901688",
      name: "Nhà số 6 (F) - SG2 Lê Thạch",
      type: "Nhà cấp III",
      value: "36,393,000",
      department: "Nhà cấp III",
      date: "01/01/1975",
    },
    {
      id: 8,
      code: "011008-203-901669",
      name: "Nhà số 1 (A), SG2 Lê Thạch",
      type: "Nhà cấp II",
      value: "4,980,549,000",
      department: "Nhà cấp II",
      date: "01/01/1919",
    },
    {
      id: 9,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      type: "Nhà cấp II",
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
    {
      id: 9,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      type: "Nhà cấp II",
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
    {
      id: 9,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      type: "Nhà cấp II",
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
    {
      id: 9,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      type: "Nhà cấp II",
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
  ];

  //   const handleChangePage = (event, newPage) => {
  //     setPage(newPage);
  //   };

  //   const handleChangeRowsPerPage = (event) => {
  //     setRowsPerPage(parseInt(event.target.value, 10));
  //     setPage(0);
  //   };

  return (
    <div className="p-4">
      <Box className="mb-4">
        <div className="flex flex-wrap gap-4">
          <div>
            <label className="block mb-1">TỪ khóa</label>
            <TextField
              placeholder="Nhập tên học mã..."
              variant="outlined"
              size="small"
              value={keyword}
              onChange={(e) => setKeyword(e.target.value)}
              className="w-64"
            />
          </div>

          <div>
            <label className="block mb-1">Loại tài sản:</label>
            <Select
              value={assetType}
              onChange={(e) => setAssetType(e.target.value)}
              displayEmpty
              variant="outlined"
              size="small"
              className="w-64"
            >
              <MenuItem value="">Tất cả</MenuItem>
              <MenuItem value="Nhà cấp I">Nhà cấp I</MenuItem>
              <MenuItem value="Nhà cấp II">Nhà cấp II</MenuItem>
              <MenuItem value="Nhà cấp III">Nhà cấp III</MenuItem>
            </Select>
          </div>

          <LocalizationProvider dateAdapter={AdapterDayjs}>
            {/* <div>
              <label className="block mb-1">Từ ngày</label>
              <DatePicker
                value={fromDate}
                onChange={(newValue) => setFromDate(newValue)}
                renderInput={(params) => <TextField {...params} size="small" />}
              />
            </div>
            <div>
              <label className="block mb-1">Đến ngày</label>
              <DatePicker
                value={toDate}
                onChange={(newValue) => setToDate(newValue)}
                renderInput={(params) => <TextField {...params} size="small" />}
              />
            </div> */}
          </LocalizationProvider>

          {/* Department Filter */}
          <div>
            <label className="block mb-1">Bộ phận sử dụng</label>
            <Select
              value={department}
              onChange={(e) => setDepartment(e.target.value)}
              displayEmpty
              variant="outlined"
              size="small"
              className="w-64"
            >
              <MenuItem value="">-- Chọn bộ phận sử dụng --</MenuItem>
              <MenuItem value="Nhà cấp I">Nhà cấp I</MenuItem>
              <MenuItem value="Nhà cấp II">Nhà cấp II</MenuItem>
              <MenuItem value="Nhà cấp III">Nhà cấp III</MenuItem>
            </Select>
          </div>
        </div>
      </Box>

      {/* Action Buttons */}
      <Box className="flex justify-end mb-4 gap-2">
        <Button variant="contained" color="primary">
          Chờ duyệt
        </Button>
        <Button variant="contained" color="error">
          Từ chối
        </Button>
        <Button variant="contained" color="success">
          Đã duyệt
        </Button>
      </Box>

      {/* Table  */}
      <Box>
        <div className="flex justify-between items-center mb-2">
          <h2 className="text-lg font-bold">DANH SÁCH TÀI SẢN CHỜ DUYỆT</h2>
          <div className="flex gap-2">
            <Button variant="outlined" startIcon={<ArrowDropDownIcon />}>
              In danh sách tài sản
            </Button>
            <Button variant="contained" startIcon={<AddIcon />}>
              Thêm mới
            </Button>
          </div>
        </div>

        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow sx={{ backgroundColor: "#2673b4", color: "white" }}>
                <TableCell>
                  <Checkbox />
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  STT
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  Mã tài sản
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  Tên tài sản
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  Loại tài sản
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  Nguyên giá
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  Bộ phận sử dụng
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  Ngày sử dụng
                </TableCell>
                <TableCell sx={{ color: "white", fontWeight: "bold" }}>
                  Thao tác
                </TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {rows
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row, index) => (
                  <TableRow key={row.id}>
                    <TableCell>
                      <Checkbox />
                    </TableCell>
                    <TableCell>{page * rowsPerPage + index + 1}</TableCell>
                    <TableCell>{row.code}</TableCell>
                    <TableCell>{row.name}</TableCell>
                    <TableCell>{row.type}</TableCell>
                    <TableCell>{row.value}</TableCell>
                    <TableCell>{row.department}</TableCell>
                    <TableCell>{row.date}</TableCell>
                    <TableCell align="center">
                      <Button
                        variant="outlined"
                        size="small"
                        startIcon={<VisibilityIcon />}
                      >
                        Xem
                      </Button>
                      <Button variant="outlined" size="small" className="ml-2">
                        In thẻ
                      </Button>
                      <Button variant="outlined" size="small" className="ml-2">
                        Sửa
                      </Button>
                      <Button variant="outlined" size="small" className="ml-2">
                        Xóa
                      </Button>
                    </TableCell>
                  </TableRow>
                ))}
            </TableBody>
          </Table>
        </TableContainer>

        {/* <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={rows.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
          labelRowsPerPage="Hiển thị"
          labelDisplayedRows={({ from, to, count }) =>
            `Hiển thị từ ${from} đến ${to} của ${count} bản ghi`
          }
        /> */}
      </Box>
    </div>
  );
};

export default AssetList;
