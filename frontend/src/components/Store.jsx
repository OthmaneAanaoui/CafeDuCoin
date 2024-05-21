import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../axiosConfig";
import {
  Container,
  Typography,
  Box,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Button,
} from "@mui/material";
import { FaGamepad } from "react-icons/fa";

const Store = () => {
  const navigate = useNavigate();
  const [rentedGames, setRentedGames] = useState([]);

  useEffect(() => {
    const fetchRentedGames = async () => {
      try {
        const response = await axiosInstance.get('/Game/games/rented');
        setRentedGames(response.data);
      } catch (error) {
        console.error("Error fetching rented games:", error);
      }
    };

    fetchRentedGames();
  }, []);

  

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  const returnGame = async (gameId) => {
    try {
      await axiosInstance.post(`/Game/games/${gameId}/return`);
      setRentedGames((prevGames) =>
        prevGames.filter((game) => game.id !== gameId)
      );
    } catch (error) {
      console.error("Error returning game:", error);
    }
  };

  return (
    <Container maxWidth="lg" className="m-2 md:m-10 mt-24 p-2 md:p-6 bg-white rounded-3xl">
      <Box className="mb-10">
        <Box className="flex gap-4">
          <Box className="text-5xl rounded-lg p-5 hover:drop-shadow-xl" style={{ color: "#000", backgroundColor: "rgb(255, 245, 252)" }}>
            <FaGamepad />
          </Box>
          <Box>
            <Typography variant="h6" color="textSecondary">Liste</Typography>
            <Typography variant="h4" fontWeight="bold">Jeux empruntés</Typography>
            <Typography variant="h5" fontWeight="bold" color="textSecondary">Café du coin</Typography>
          </Box>
        </Box>
        <Button variant="contained" color="secondary" onClick={handleLogout} sx={{ mt: 2 }}>Quitter</Button>
        <Button variant="outlined" color="secondary" href="../home" sx={{ mt: 2, ml: 2 }}>Retour</Button>
      </Box>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Nom</TableCell>
              <TableCell align="right">Description</TableCell>
              <TableCell align="right">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {rentedGames.map((rental) => (
              <TableRow key={rental.id} sx={{ "&:last-child td, &:last-child th": { border: 0 } }}>
                <TableCell component="th" scope="row">{rental.game.name}</TableCell>
                <TableCell align="right">{rental.game.description}</TableCell>
                <TableCell align="right">
                {!rental.returnDate ? (
                    <Button
                      onClick={() => returnGame(rental.game.id)}
                      variant="outlined"
                      sx={{ mt: 1 }}
                    >
                      Retourner ce jeu
                    </Button>
                  ) : (
                    "Déjà retourné"
                  )}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default Store;
