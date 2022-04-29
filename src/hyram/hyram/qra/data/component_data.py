"""
Copyright 2015-2022 National Technology & Engineering Solutions of Sandia, LLC (NTESS).
Under the terms of Contract DE-NA0003525 with NTESS, the U.S. Government retains certain rights in this software.

You should have received a copy of the GNU General Public License along with HyRAM+.
If not, see https://www.gnu.org/licenses/.
"""

"""
Lognormal distribution parameters for component leak frequencies based on fuel and phase.
Dict format is <component name>: [<leak_size> [mu, sigma], ...] where leak sizes are 0.01%, 0.1%, 1%, 10%, 100%.
"""


h2_gas_params = {
    'compressor': [
        [-2.3053, 0.3018],
        [-4.0761, 0.5249],
        [-5.3881, 0.8212],
        [-8.7929, 0.7263],
        [-11.1359, 1.2067],
    ],
    'vessel': [
        [-13.4770, 0.7347],
        [-13.6410, 0.6387],
        [-14.0512, 0.6203],
        [-14.6144, 0.6041],
        [-15.2732, 0.6156],
    ],
    'filter': [
        [-5.2348, 1.6955],
        [-5.2822, 1.2885],
        [-5.3303, 1.2879],
        [-5.3798, 0.7448],
        [-5.4288, 0.8171],
    ],
    'flange': [
        [-3.9125, 1.4920],
        [-6.1191, 1.1345],
        [-8.3252, 2.0541],
        [-10.5327, 0.7208],
        [-12.7385, 1.6925],
    ],
    'hose': [
        [-7.4534, 0.3863],
        [-8.5011, 0.6050],
        [-8.7103, 0.6106],
        [-8.8001, 0.5901],
        [-9.6934, 0.9836],
    ],
    'joint': [
        [-10.2591, 0.2423],
        [-12.2703, 0.8727],
        [-11.7538, 0.5333],
        [-11.7961, 0.6073],
        [-11.9590, 0.6600],
    ],
    'pipe': [
        [-11.7331, 0.7162],
        [-12.5079, 0.7070],
        [-13.8601, 1.2515],
        [-14.5893, 1.1933],
        [-15.7354, 1.8486],
    ],
    'valve': [
        [-5.8546, 0.2500],
        [-7.4425, 0.4344],
        [-9.8190, 1.1434],
        [-10.6079, 0.6270],
        [-12.2436, 1.3690],
    ],
    'instrument': [
        [-7.3800, 0.7100],
        [-8.5400, 0.8500],
        [-9.1000, 0.9200],
        [-9.2100, 1.0900],
        [-10.2100, 1.4900],
    ],
    'exchanger': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'vaporizer': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'arm': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'extra1': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'extra2': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
}

h2_liquid_params = {
    'compressor': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'vessel': [
        [-7.3426, 1.7799],
        [-8.8915, 2.5535],
        [-10.4746, 2.0526],
        [-12.0829, 2.7255],
        [-13.6552, 3.1272],
    ],
    'filter': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'flange': [
        [-3.9125, 1.4920],
        [-6.1191, 1.1345],
        [-8.3252, 2.0541],
        [-10.5327, 0.7208],
        [-12.7385, 1.6925],
    ],
    'hose': [
        [-7.4534, 0.3863],
        [-8.5011, 0.6050],
        [-8.7103, 0.6106],
        [-8.8001, 0.5901],
        [-9.6934, 0.9836],
    ],
    'joint': [
        [-10.2591, 0.2423],
        [-12.2703, 0.8727],
        [-11.7538, 0.5333],
        [-11.7961, 0.6073],
        [-11.9590, 0.6600],
    ],
    'pipe': [
        [-11.7331, 0.7162],
        [-12.5079, 0.7070],
        [-13.8601, 1.2515],
        [-14.5893, 1.1933],
        [-15.7354, 1.8486],
    ],
    'valve': [
        [-5.8546, 0.2500],
        [-7.4425, 0.4344],
        [-9.8190, 1.1434],
        [-10.6079, 0.6270],
        [-12.2436, 1.3690],
    ],
    'instrument': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'exchanger': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'vaporizer': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'arm': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'extra1': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'extra2': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
}

ch4_gas_params = {
    'compressor': [
        [0.7785, 1.3159],
        [-2.2394, 0.9997],
        [-5.2560, 1.0068],
        [-8.2731, 0.7015],
        [-11.2905, 1.2350],
    ],
    'vessel': [
        [-0.4139, 1.3445],
        [-3.8954, 1.0446],
        [-7.3613, 0.8054],
        [-10.8805, 0.6776],
        [-14.3160, 0.6934],
    ],
    'filter': [
        [-5.2348, 1.6955],
        [-5.2822, 1.2885],
        [-5.3303, 1.2879],
        [-5.3798, 0.7448],
        [-5.4288, 0.8171],
    ],
    'flange': [
        [-3.9125, 1.4920],
        [-6.1191, 1.1345],
        [-8.3252, 2.0541],
        [-10.5327, 0.7208],
        [-12.7385, 1.6925],
    ],
    'hose': [
        [2.5434, 1.2507],
        [0.3455, 0.9447],
        [-1.8439, 0.7670],
        [-4.0745, 0.6712],
        [-6.2263, 1.3906],
    ],
    'joint': [
        [-0.6255, 1.2727],
        [-2.3062, 0.9835],
        [-4.0101, 0.9541],
        [-5.6481, 0.5745],
        [-7.3739, 0.6569],
    ],
    'pipe': [
        [-7.9272, 0.9882],
        [-9.6818, 0.7884],
        [-11.4317, 1.5059],
        [-13.2003, 1.2938],
        [-14.9400, 2.1066],
    ],
    'valve': [
        [-4.4577, 1.0301],
        [-6.2603, 0.8332],
        [-8.0664, 1.5396],
        [-9.8503, 0.6458],
        [-11.6738, 1.4350],
    ],
    'instrument': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'exchanger': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'vaporizer': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'arm': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'extra1': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'extra2': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
}

ch4_liquid_params = {
    'compressor': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'vessel': [
        [-7.6443, 1.1463],
        [-8.8817, 2.2191],
        [-10.1486, 1.9222],
        [-11.4202, 2.4219],
        [-12.6988, 3.1816],
    ],
    'filter': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'flange': [
        [-10.0826, 0.7306],
        [-10.6980, 1.2105],
        [-11.1787, 2.3964],
        [-11.6591, 2.7634],
        [-12.1601, 2.9150],

    ],
    'hose': [
        [-13.3971, 0.7419],
        [-11.7497, 0.5645],
        [-10.0943, 4.1882],
        [-8.4524, 0.9343],
        [-6.8075, 3.6381],
    ],
    'joint': [
        [10.4669, 2.1960],
        [6.1666, 1.6594],
        [1.8652, 1.1464],
        [-2.4345, 0.7068],
        [-6.7363, 0.6198],
    ],
    'pipe': [
        [-12.8352, 1.3156],
        [-13.4479, 1.4332],
        [-14.0571, 1.1603],
        [-14.6718, 1.3553],
        [-15.2867, 1.8314],
    ],
    'valve': [
        [-9.3821, 0.7206],
        [-10.0776, 0.9821],
        [-10.7378, 1.1534],
        [-11.3422, 1.9367],
        [-11.9495, 1.9345],
    ],
    'instrument': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'exchanger': [
        [-6.0563, 0.9571],
        [-7.0191, 1.3035],
        [-8.0347, 1.4167],
        [-9.0535, 2.3068],
        [-10.0834, 1.6190],
    ],
    'vaporizer': [
        [-4.8184, 2.5489],
        [-3.6464, 1.8695],
        [-2.4760, 1.2243],
        [-1.3037, 0.7074],
        [-0.1328, 0.7068],
    ],
    'arm': [
        [-1.6169, 3.0126],
        [-4.3987, 2.0841],
        [-7.2019, 1.8945],
        [-10.3173, 1.0339],
        [-12.7037, 3.3875],

    ],
    'extra1': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
    'extra2': [
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
        [999., 999.],
    ],
}

c3h8_gas_params = ch4_gas_params

c3h8_liquid_params = c3h8_gas_params


def get_leak_frequency_set_for_species(species='h2', saturated_phase=None):
    species = species.lower()

    if species in ['h2', 'hydrogen']:
        leak_params = h2_liquid_params if saturated_phase in ['gas', 'liquid'] else h2_gas_params

    elif species in ['ch4', 'methane']:
        leak_params = ch4_liquid_params if saturated_phase in ['gas', 'liquid'] else ch4_gas_params

    elif species in ['c3h8', 'propane']:
        leak_params = c3h8_liquid_params if saturated_phase in ['gas', 'liquid'] else c3h8_gas_params

    else:
        raise ValueError

    return leak_params


def get_component_leak_parameters(component, species='h2', saturated_phase=None):
    """
    Obtain dict of leak frequency distribution parameters for specified components.

    phase : None or str
        CoolProp parameter for fuel phase.
        Note that these values may be unintuitive. 'gas' and 'liquid' indicate saturated state.
        Typical h2 gas should have parameter value other than these.

    """
    leak_params = get_leak_frequency_set_for_species(species=species, saturated_phase=saturated_phase)
    return leak_params[component]
