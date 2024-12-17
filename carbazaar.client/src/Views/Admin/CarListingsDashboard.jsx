import React, { useEffect, useState } from "react";
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TablePagination, Paper } from "@mui/material";
import api from "../../api/api";

const CarListingsDashboard = () => {
  const [carListings, setCarListings] = useState([]);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(0);
  const [pageSize, setPageSize] = useState(5);
  const [totalPages, setTotalPages] = useState(0); 

  const fetchCarListings = async (page, pageSize) => {
    try {
      const response = await api.get("/Admin/get-listings", {
        params: { page: page + 1, pageSize },
      });

      setCarListings(response.data.items); 
      setTotalPages(response.data.totalPages || 0);
    } catch (error) {
      console.error("Failed to fetch car listings:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchCarListings(page, pageSize);
  }, [page, pageSize]);

  const handlePageChange = (event, newPage) => {
    setPage(newPage);
  };

  const handlePageSizeChange = (event) => {
    setPageSize(parseInt(event.target.value, 10));
    setPage(0);
  };

  return (
    <Paper>
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Price</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={2} align="center">Loading...</TableCell>
              </TableRow>
            ) : (
              carListings.map((car) => (
                <TableRow key={car.id}>
                  <TableCell>{car.id}</TableCell>
                  <TableCell>{car.name}</TableCell>
                  <TableCell>{car.price}</TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        component="div"
        count={totalPages * pageSize}
        page={page}
        onPageChange={handlePageChange}
        rowsPerPage={pageSize}
        onRowsPerPageChange={handlePageSizeChange}
        rowsPerPageOptions={[5, 10, 25]}
      />
    </Paper>
  );
};

export default CarListingsDashboard;