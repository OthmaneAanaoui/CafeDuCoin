import React, { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import axiosInstance from "../axiosConfig";
import {
  Container,
  Typography,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Grid,
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
import { FcApproval, FcCancel } from "react-icons/fc";

import { BsPencilSquare } from "react-icons/bs";

const Home = () => {
  const [games, setGames] = useState([]);
  const [state, setState] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchGames = async () => {
      try {
        const response = await axiosInstance.get("/Game/games");
        setGames(response.data);
      } catch (error) {
        console.error("Error fetching games:", error);
      }
    };

    fetchGames();
  }, []);

  const filterGames = () => {
    if (state === null) return games;
    return games.filter((game) => game.isAvailable === state);
  };

  const filteredGames = filterGames();

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  const addToMyList = async (gameId) => {
    try {
      await axiosInstance.post(`/Game/games/${gameId}/rent`);
     
      setGames((prevGames) =>
        prevGames.map((game) =>
          game.id === gameId ? { ...game, isAvailable: false } : game
        )
      );
    } catch (error) {
      console.error("Error renting game:", error);
      alert("Failed to rent game.");
    }
  };

  return (
    <>
      <Container maxWidth="lg" className="m-2 md:m-10 mt-24 p-2 md:p-6 bg-white rounded-3xl">
        <Box className="mb-10">
          {/* Header */}
          <Box className="flex gap-4">
            <Box className="text-5xl rounded-lg p-5 hover:drop-shadow-xl" style={{ color: "#000", backgroundColor: "rgb(255, 245, 252)" }}>
              <FaGamepad />
            </Box>
            <Box>
              <Typography variant="h6" color="textSecondary">Liste</Typography>
              <Typography variant="h4" fontWeight="bold">Jeux</Typography>
              <Typography variant="h5" fontWeight="bold" color="textSecondary">Café du coin</Typography>
            </Box>
          </Box>
          {/* Logout button */}
          <Button variant="contained" color="secondary" onClick={handleLogout} sx={{ mt: 2 }}>Quitter</Button>
          <Button variant="outlined" color="secondary" href="../store" sx={{ mt: 2 }}>Ma liste</Button>
        </Box>

        <Box sx={{ my: 3, mx: 2 }}>
          {/* Filter dropdown */}
          <Grid container alignItems="center">
            <Grid item xs={12} md={2}>
              <FormControl fullWidth>
                <InputLabel id="state-label">État</InputLabel>
                <Select
                  labelId="state-label"
                  id="state"
                  value={state}
                  onChange={(event) => setState(event.target.value)}
                  label="État"
                >
                  <MenuItem value={null}><em>Vide</em></MenuItem>
                  <MenuItem value={true}>Disponible</MenuItem>
                  <MenuItem value={false}>Emprunté</MenuItem>
                </Select>
              </FormControl>
            </Grid>
          </Grid>
        </Box>

        {/* Game list */}
        <TableContainer component={Paper}>
          <Table sx={{ minWidth: 650 }} aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell>Nom</TableCell>
                <TableCell align="right">Description</TableCell>
                <TableCell align="right">État</TableCell>
                <TableCell align="right">Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {filteredGames.map((game) => (
                <TableRow key={game.id} sx={{ "&:last-child td, &:last-child th": { border: 0 } }}>
                  <TableCell component="th" scope="row">{game.name}</TableCell>
                  <TableCell align="right">{game.description}</TableCell>
                  <TableCell align="right">
                    {game.isAvailable ? (
                      <>
                        <FcApproval />
                        <span>Disponible</span>
                      </>
                    ) : (
                      <>
                        <FcCancel />
                        <span>Emprunté</span>
                      </>
                    )}
                  </TableCell>
                  <TableCell align="right">
                    {game.isAvailable && (
                      <Button
                        onClick={() => addToMyList(game.id)}
                        variant="outlined"
                        sx={{ mt: 1 }}
                      >
                        Emprunter ce jeu
                      </Button>
                    )}

                    <Link
                    target="_blank"
                    to={{
                      pathname: `/details/${game.id}`,
                    }}
                  >
                    <Button style={{ color: "white" }} variant="contained">
                      <BsPencilSquare /> Historique
                    </Button>
                  </Link>
        
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Container>
    </>
  );
};

export default Home;
