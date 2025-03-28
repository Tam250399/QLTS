import React, { ChangeEvent, useState } from "react";
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
  TablePagination,
  Autocomplete,
  SelectChangeEvent,
} from "@mui/material";

import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import VisibilityIcon from "@mui/icons-material/Visibility";
import AddIcon from "@mui/icons-material/Add";
import ArrowDropDownIcon from "@mui/icons-material/ArrowDropDown";

interface Row {
  id: number;
  code: string;
  name: string;
  type: string;
  value: string;
  department: string;
  date: string;
  idType: number;
}

interface AssetTypeOption {
  type: string;
  id: number;
}

// Định nghĩa các tùy chọn loại tài sản
const assetTypeOptions: AssetTypeOption[] = [
  { type: "Tất cả", id: 0 },
  { type: "Nhà cấp I", id: 1 },
  { type: "Nhà cấp II", id: 2 },
  { type: "Nhà cấp III", id: 3 },
];

const AssetList = () => {
  // Data fake
  const rows: Row[] = [
    {
      id: 1,
      code: "011008-203-901685",
      name: "Nhà số 3 (C), SG2 Lê Thạch",
      type: "Nhà cấp 2",
      idType: 2,
      value: "6,068,825,000",
      department: "Nhà cấp II",
      date: "13/03/2019",
    },
    {
      id: 2,
      code: "011008-204-901689",
      name: "Nhà số 8 (M) - SG2 Lê Thạch",
      type: "Nhà cấp 3",
      idType: 3,
      value: "21,852,000",
      department: "Nhà cấp III",
      date: "01/01/1993",
    },
    {
      id: 3,
      code: "011008-204-901671",
      name: "Nhà số 9 (N), SG2 Lê Thạch",
      type: "Nhà cấp 3",
      idType: 3,
      value: "10,926,000",
      department: "Nhà cấp III",
      date: "01/01/1992",
    },
    {
      id: 4,
      code: "011008-203-901686",
      name: "Nhà số 4 (D), SG2 Lê Thạch",
      type: "Nhà cấp 2",
      idType: 2,
      value: "874,676,545",
      department: "Nhà cấp II",
      date: "01/01/1982",
    },
    {
      id: 5,
      code: "011008-204-901670",
      name: "Nhà số 7 (H), SG2 Lê Thạch",
      type: "Nhà cấp 3",
      idType: 3,
      value: "25,092,000",
      department: "Nhà cấp III",
      date: "01/01/1978",
    },
    {
      id: 6,
      code: "011008-204-901684",
      name: "Nhà số 2 (B), SG2 Lê Thạch",
      type: "Nhà cấp 1",
      idType: 1,
      value: "11,334,281,095",
      department: "Nhà cấp I",
      date: "01/01/1975",
    },
    {
      id: 7,
      code: "011008-204-901688",
      name: "Nhà số 6 (F) - SG2 Lê Thạch",
      type: "Nhà cấp 3",
      idType: 3,
      value: "36,393,000",
      department: "Nhà cấp III",
      date: "01/01/1975",
    },
    {
      id: 8,
      code: "011008-203-901669",
      name: "Nhà số 1 (A), SG2 Lê Thạch",
      type: "Nhà cấp 2",
      value: "4,980,549,000",
      idType: 0,
      department: "Nhà cấp II",
      date: "01/01/1919",
    },
    {
      id: 9,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      idType: 2,
      type: "Nhà cấp 2",
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
    {
      id: 11,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      type: "Nhà cấp 2",
      idType: 2,
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
    {
      id: 12,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      type: "Nhà cấp 2",
      idType: 2,
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
    {
      id: 10,
      code: "011008-203-901687",
      name: "Nhà số 5 (E), SG2 Lê Thạch",
      type: "Nhà cấp 2",
      idType: 2,
      value: "2,050,880,818",
      department: "Nhà cấp II",
      date: "01/01/1918",
    },
  ];

  const [keyword, setKeyword] = useState("");
  const [department, setDepartment] = useState("");
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [selected, setSelected] = useState<number[]>([]);
  const [selectedAssetTypes, setSelectedAssetTypes] = useState<
    AssetTypeOption[]
  >([assetTypeOptions[0]]);
  const [filteredData, setFilteredData] = useState<Row[]>(rows);

  const handleSelectAllClick = (event: ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
      const newSelected = rows.map((row) => row.id);
      setSelected(newSelected);
      return;
    }
    setSelected([]);
  };

  const handleClick = (_event: React.MouseEvent<unknown>, id: number) => {
    const selectedIndex = selected.indexOf(id);
    let newSelected: number[] = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }

    setSelected(newSelected);
  };

  const handleChangePage = (_: unknown, newPage: number) => setPage(newPage);

  const handleChangeRowsPerPage = (event: ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const isSelected = (id: number): boolean => selected.indexOf(id) !== -1;
  const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

  //hàm tìm kiếm chung
  const handleSearch = () => {
    const results = rows.filter((item) => {
      const matchesKeyword = item.name
        .toLowerCase()
        .includes(keyword.toLowerCase().trim());
      const matchesAssetType = selectedAssetTypes.some(
        (selected) => selected.id === 0 || selected.id === item.idType
      );
      const matchesDepartment = !department || item.department === department;

      return matchesKeyword && matchesAssetType && matchesDepartment;
    });

    setFilteredData(results);
    setPage(0);
  };

  // Xử lý khi thay đổi loại tài sản
  const handleAssetTypeChange = (
    _: React.SyntheticEvent,
    newValue: AssetTypeOption[]
  ) => {
    // Nếu không có lựa chọn nào, mặc định chọn "Tất cả"
    if (newValue.length === 0) {
      setSelectedAssetTypes([assetTypeOptions[0]]);
      return;
    }

    // Kiểm tra xem có đang chọn "Tất cả" không
    const hasAllOption = newValue.some((option) => option.id === 0);

    // Nếu đang chọn "Tất cả" và có thêm lựa chọn khác
    if (hasAllOption && newValue.length > 1) {
      // Bỏ "Tất cả" và chỉ giữ lại các lựa chọn khác
      const filteredOptions = newValue.filter((option) => option.id !== 0);
      setSelectedAssetTypes(filteredOptions);
    }

    // Nếu chọn "Tất cả" khi đã có lựa chọn khác
    else if (hasAllOption) {
      // Chỉ giữ lại "Tất cả"
      setSelectedAssetTypes([assetTypeOptions[0]]);
    }
    // Nếu chọn các lựa chọn khác
    else {
      setSelectedAssetTypes(newValue);
    }

    // Tự động tìm kiếm khi thay đổi loại tài sản
    handleSearch();
  };

  // Xử lý khi thay đổi từ khóa
  const handleKeywordChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setKeyword(e.target.value);
    handleSearch();
  };

  // Xử lý khi thay đổi bộ phận
  const handleDepartmentChange = (e: SelectChangeEvent) => {
    setDepartment(e.target.value);
    handleSearch();
  };

  // Xử lý khi thay đổi trạng thái
  const handleStatusChange = (status: "pending" | "rejected" | "approved") => {
    setCurrentStatus(status);
    handleSearch();
  };

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
              onChange={handleKeywordChange}
              className="w-64"
            />
          </div>

          <div>
            <label className="block mb-1">Loại tài sản:</label>
            <Autocomplete
              multiple
              limitTags={1}
              id="multiple-limit-tags"
              options={assetTypeOptions}
              getOptionLabel={(option) => option.type}
              value={selectedAssetTypes}
              onChange={handleAssetTypeChange}
              isOptionEqualToValue={(option, value) =>
                option.type === value.type && option.id === value.id
              }
              renderInput={(params) => <TextField {...params} />}
              sx={{ width: "400px", height: "10px" }}
            />
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
              onChange={handleDepartmentChange}
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
          <Button
            variant="contained"
            sx={{
              marginTop: "30px",
              height: "30px",
              fontWeight: "200",
              textTransform: "none",
            }}
            onClick={handleSearch}
          >
            Tìm kiếm
          </Button>
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
                <TableCell padding="checkbox">
                  <Checkbox
                    color="primary"
                    indeterminate={
                      selected.length > 0 && selected.length < rows.length
                    }
                    checked={rows.length > 0 && selected.length === rows.length}
                    onChange={handleSelectAllClick}
                    inputProps={{
                      "aria-label": "select all assets",
                    }}
                  />
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
              {filteredData
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row) => {
                  const isItemSelected = isSelected(row.id);
                  return (
                    <TableRow
                      hover
                      onClick={(event) => handleClick(event, row.id)}
                      role="checkbox"
                      aria-checked={isItemSelected}
                      tabIndex={-1}
                      key={row.id}
                      selected={isItemSelected}
                    >
                      <TableCell padding="checkbox">
                        <Checkbox
                          color="primary"
                          checked={isItemSelected}
                          inputProps={{
                            "aria-labelledby": `checkbox-${row.id}`,
                          }}
                        />
                      </TableCell>
                      <TableCell>{row.id}</TableCell>
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
                        <Button
                          variant="outlined"
                          size="small"
                          className="ml-2"
                        >
                          In thẻ
                        </Button>
                        <Button
                          variant="outlined"
                          size="small"
                          className="ml-2"
                        >
                          Sửa
                        </Button>
                        <Button
                          variant="outlined"
                          size="small"
                          className="ml-2"
                        >
                          Xóa
                        </Button>
                      </TableCell>
                    </TableRow>
                  );
                })}
              {emptyRows > 0 && (
                <TableRow style={{ height: 53 * emptyRows }}>
                  <TableCell colSpan={6} />
                </TableRow>
              )}
            </TableBody>
          </Table>
        </TableContainer>

        <TablePagination
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
        />
      </Box>
    </div>
  );
};

export default AssetList;
