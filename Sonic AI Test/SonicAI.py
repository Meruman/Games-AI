import retro
import numpy as np
import cv2
import neat
import pickle

env = retro.make('SonicTheHedgehog-Genesis', 'GreenHillZone.Act1')

imgarray = []

ac = env.action_space.sample()
inx, iny, inc = env.observation_space.shape

print(inx,iny,inc)

def eval_genomes(genomes, config):
   
    for genome_id, genome in genomes:
        ob = env.reset()
        ac = env.action_space.sample()

        inx, iny, inc = env.observation_space.shape

        inx = int(inx/8)
        iny = int(iny/8)

        net = neat.nn.recurrent.RecurrentNetwork.create(genome, config)

        current_max_fitness = 0
        fitness_current = 0
        frame = 0
        counter = 0
        xpos = 0
        xpos_max = 0

        done = False

        while not done:

            env.render()
            frame +=1

            ob = cv2.resize(ob, (inx, iny))
            ob = cv2.cvtColor(ob, cv2.COLOR_BGR2GRAY)
            ob = np.reshape(ob, (inx,iny))

            imgarray = np.ndarray.flatten(ob)

            nnOutput = net.activate(imgarray)

            print(nnOutput)

config = neat.Config(neat.DefaultGenome, neat.DefaultReproduction,
                     neat.DefaultSpeciesSet, neat.DefaultStagnation,
                     'config-feedforward')


p = neat.Population(config)

winner = p.run(eval_genomes)